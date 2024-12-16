using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.Contracts
{
    internal interface ICanvaControlItem
    {
        Control control { get; }
        int x { get; }
        int y { get; }
        int z { get; }
    }
}
