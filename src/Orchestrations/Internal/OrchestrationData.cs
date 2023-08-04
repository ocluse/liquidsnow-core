using System.Collections.Generic;

namespace Ocluse.LiquidSnow.Core.Orchestrations.Internal
{
    internal class OrchestrationData<T> : IOrchestrationData<T>
    {
        private readonly List<OrchestrationStepResult> _results;
        public int CurrentStep { get; private set; }
        public IReadOnlyList<OrchestrationStepResult> Results => _results;
        public IOrchestrationBag Bag { get; }

        public T Orchestration { get; }

        public int? CurrentOrder { get; private set; }

        public int StepCount { get; }

        public OrchestrationData(T value, int stepCount)
        {
            _results = new List<OrchestrationStepResult>();
            Bag = new OrchestrationBag();
            Orchestration = value;
            StepCount = stepCount;
        }


        public void Advance(OrchestrationStepResult result, int? order)
        {
            _results.Add(result);
            CurrentStep++;
            CurrentOrder = order;
        }
    }
}
