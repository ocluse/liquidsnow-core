using Ocluse.LiquidSnow.Core.Cqrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CqrsSample
{
    internal record SampleCommand(string Message) : ICommand
    {
    }
}
