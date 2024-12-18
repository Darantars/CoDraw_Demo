using Avalonia.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.Models
{
    public class CanvaControlItem : INotifyPropertyChanged
    {

        private double x { get; set; }
        private double y { get; set; }
        private double z { get; set; }
        
        public Control control { get; }
        public double X
        {
            get
            {
                return x;
            }
            set
            {
                x = value;
                OnPropertyChanged(nameof(X));
            }
        }
        
        public double Y
        {
            get
            {
                return y;
            }
            set
            {
                y = value;
                OnPropertyChanged(nameof(Y));
            }
        }
        
        public double Z
        {
            get
            {
                return z;
            }
            set
            {
                z = value;
                OnPropertyChanged(nameof(Z));
            }
        }


        public string name { get; }
        public double width { get; }
        public double height { get; }

        public CanvaControlItem(double controlX, double controlY, double controlZ, Control innerControl)
        {
            control = innerControl;
            X = controlX;
            y = controlY;
            z = controlZ;
            width = control.Width;
            height = control.Height;
            name = Guid.NewGuid().ToString(); 
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
