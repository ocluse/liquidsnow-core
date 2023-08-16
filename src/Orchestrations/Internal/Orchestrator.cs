﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using System.Threading;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;

namespace Ocluse.LiquidSnow.Core.Orchestrations.Internal
{
    internal class Orchestrator : IOrchestrator
    {
        private readonly IServiceProvider _serviceProvider;

        public Orchestrator(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        private async Task<TResult> Execute<T, TResult>(T value, CancellationToken cancellationToken = default)
            where T : IOrchestration<TResult>
        {
            Type orchestrationType = value.GetType();

            Type[] typeArgs = { orchestrationType, typeof(TResult) };

            Type orchestrationStepType = typeof(IOrchestrationStep<,>).MakeGenericType(typeArgs);

            List<IOrchestrationStep<T, TResult>> steps = _serviceProvider
                .GetServices(orchestrationStepType)
                .Cast<IOrchestrationStep<T, TResult>>()
                .OrderBy(x => x.Order)
                .ToList();

            if (steps.Count == 0)
            {
                throw new InvalidOperationException($"No steps found for orchestration {orchestrationType.Name}");
            }

            var preliminaryStep = _serviceProvider.GetService<IPreliminaryOrchestrationStep<T, TResult>>();

            var data = new OrchestrationData<T>(value);

            int order = steps[0].Order;

            RequiredState? previousState = null;

            if (preliminaryStep != null)
            {
                IOrchestrationStepResult result = await preliminaryStep.Execute(data, cancellationToken);
                data.Advance(result);

                previousState = result.IsSuccess ? RequiredState.Success : RequiredState.Failure;

                if (result.JumpToOrder != null)
                {
                    order = result.JumpToOrder.Value;
                }
            }

            while (true)
            {
                cancellationToken.ThrowIfCancellationRequested();

                IOrchestrationStep<T, TResult> step = steps.FirstOrDefault(x => x.Order == order)
                    ?? throw new InvalidOperationException($"No step found with order {order}");

                bool canExecute = true;

                if (step is IStateDependentOrchestrationStep<T, TResult> stateDependentStep)
                {
                    canExecute = previousState == stateDependentStep.RequiredState;
                }

                if (canExecute)
                {
                    if (step is IConditionalOrchestrationStep<T, TResult> conditionalStep)
                    {
                        canExecute = await conditionalStep.CanExecute(data, cancellationToken);
                    }

                    int? jumpToOrder = null;

                    if (canExecute)
                    {
                        IOrchestrationStepResult result = await step.Execute(data, cancellationToken);
                        jumpToOrder = result.JumpToOrder;
                        data.Advance(result);
                        previousState = result.IsSuccess ? RequiredState.Success : RequiredState.Failure;
                    }

                    if (jumpToOrder != null)
                    {
                        order = jumpToOrder.Value;
                    }
                    else if (steps[^1] == step)
                    {
                        break;
                    }
                    else
                    {
                        // set order to the next step
                        order = steps[steps.IndexOf(step) + 1].Order;
                    }
                }
            }

            var finalStep = _serviceProvider.GetService<IFinalOrchestrationStep<T, TResult>>();

            if (finalStep != null)
            {
                return await finalStep.Execute(data, cancellationToken);
            }
            else
            {
                if (data.Results.Count == 0)
                {
                    throw new InvalidOperationException($"The orchestration {orchestrationType.Name} produced no results after completion.");
                }
                if (data.Results[^1] is TResult result)
                {
                    return result;
                }
                else
                {
                    throw new InvalidOperationException($"Final step did not return a result of type {typeof(TResult).Name}");
                }
            }
        }

        public Task<T> Execute<T>(IOrchestration<T> value, CancellationToken cancellationToken = default)
        {
            Type orchestrationType = value.GetType();

            Type[] typeArgs = { orchestrationType, typeof(T) };

            var methodInfo = GetType().GetMethod(nameof(Execute), BindingFlags.NonPublic | BindingFlags.Instance);

            var genericMethodInfo = methodInfo!.MakeGenericMethod(typeArgs);

            return (Task<T>)genericMethodInfo.Invoke(this, new object[] { value, cancellationToken })!;
        }
    }
}
