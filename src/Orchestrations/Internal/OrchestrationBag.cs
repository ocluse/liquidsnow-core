using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Ocluse.LiquidSnow.Core.Orchestrations.Internal
{
    internal class OrchestrationBag : Dictionary<string, object?>, IOrchestrationBag
    {

        [return: MaybeNull]
        public T Get<T>(string key)
        {
            if (ContainsKey(key))
            {
                return (T)this[key];
            }
            else
            {
                return default;
            }
        }

        public void Set<T>(string key, [AllowNull] T value)
        {
            this[key] = value;
        }
    }
}
