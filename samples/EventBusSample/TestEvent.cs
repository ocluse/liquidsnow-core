using Ocluse.LiquidSnow.Core.Events;

namespace EventBusSample
{
    public record TestEvent : IEvent
    {
        public required DateTime Timestamp { get; set; }
        public required string Message { get; set; }
    }
}
