using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class ControlPropertyViewModel : ViewModelBase
    {
        private Control _selectedControl;
        private string _selectedControlName;
        private double _selectedControlWidth;
        private double _selectedControlHeight;
        private double _selectedControlX;
        private double _selectedControlY;
        private double _selectedControlOpacity;

        public Control SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                _selectedControl = value;
            }
        }
        
        public string SelectedControlName
        {
            get
            {
                return _selectedControlName;
            }
            set
            {
                _selectedControlName = value;
            }
        }

        public double SelectedControlWidth
        {
            get
            {
                return _selectedControlWidth;
            }
            set
            {
                _selectedControlWidth = value;
            }
        }

        public double SelectedControlHeight
        {
            get
            {
                return _selectedControlHeight;
            }
            set
            {
                _selectedControlHeight = value;
            }
        }

        public double SelectedControlX
        {
            get
            {
                return _selectedControlX;
            }
            set
            {
                _selectedControlX = value;
            }
        }

        public double SelectedControlY
        {
            get
            {
                return _selectedControlY;
            }
            set
            {
                _selectedControlY = value;
            }
        }

        public double SelectedControlOpacity
        {
            get
            {
                return _selectedControlOpacity;
            }
            set
            {
                _selectedControlOpacity = value;
            }
        }


       
        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            //DesignCanvas.Children.Remove(selectedControl);
        }

        public ControlPropertyViewModel(MainConfiguratorViewModel mainConfiguratorViewModel) 
        { 
        
        }
    }
}
