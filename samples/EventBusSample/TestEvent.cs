using Ocluse.LiquidSnow.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBusSample
{
    public record TestEvent : IEvent
    {
        public required DateTime Timestamp { get; set; }
        public required string Message { get; set; }
    }
}
