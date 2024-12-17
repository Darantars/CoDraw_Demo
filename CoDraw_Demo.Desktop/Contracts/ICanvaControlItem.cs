using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.Contracts
{
    public class CanvaControlItem   
    {
        Control control { get; }
        double x { get; }
        double y { get; }
        double z { get; }

        public CanvaControlItem (double controlX, double controlY, double controlZ, Control innerControl) 
        {
            control = innerControl;
            x = controlX;
            y = controlY;
            z = controlZ;
            
        }
    }
}
