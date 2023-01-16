namespace Ocluse.LiquidSnow.Core.DependencyInjection
{
    internal class PublishStrategyConfig
    {
        public PublishStrategy Strategy { get; }

        public PublishStrategyConfig(PublishStrategy strategy)
        {
            Strategy = strategy;
        }
    }
}
