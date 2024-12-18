using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using ReactiveUI;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class ControlPropertyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private MainConfiguratorViewModel mainConfiguratorViewModel;
        private Control _selectedControl;
        private string _selectedControlName;
        private double _selectedControlWidth;
        private double _selectedControlHeight;
        private double _selectedControlX;
        private double _selectedControlY;
        private int _selectedControlZ;
        private double _selectedControlOpacity;

        public Control SelectedControl
        {
            get
            {
                return _selectedControl;
            }
            set
            {
                if (value != null)
                {
                    _selectedControl = value;
                    SelectedControlName = value.Name;
                    SelectedControlWidth = value.Width;
                    SelectedControlHeight = value.Height;
                    //Canvas parentCanvas = value.Parent as Canvas;
                    SelectedControlX = Canvas.GetLeft(value);
                    SelectedControlY = Canvas.GetTop(value);
                    SelectedControlZ = value.ZIndex;
                    
                }
                else
                { 
                    _selectedControl = null;
                    SelectedControlName = "";
                    SelectedControlWidth = 0;
                    SelectedControlHeight = 0;
                    SelectedControlX = 0;
                    SelectedControlY = 0;
                    SelectedControlZ = 0;
                }
            }
        }
        
        public string SelectedControlName
        {
            get => _selectedControlName;
            set
            {
                if (value != null)
                {
                    _selectedControlName = value;
                    OnPropertyChanged();  
                }
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
                OnPropertyChanged();  
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
                OnPropertyChanged();  
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
                if (SelectedControl != null)
                    Canvas.SetLeft(SelectedControl, value);
                _selectedControlX = value;
                OnPropertyChanged();  
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
                if (SelectedControl != null)
                    Canvas.SetTop(SelectedControl, value);
                _selectedControlY = value;
                OnPropertyChanged();  
            }
        }
        
        public int SelectedControlZ
        {
            get
            {
                return _selectedControlZ;
            }
            set
            {
                if (SelectedControl != null)
                    SelectedControl.ZIndex = value;
                _selectedControlZ = value;
                OnPropertyChanged();  
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
                OnPropertyChanged();  
            }
        }


        public ReactiveCommand<Unit, Unit> DeleteControlCommand { get; set; }
        
        private void OnDeleteClick()
        {
            mainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas
                .Children
                .Remove(
                    mainConfiguratorViewModel
                        .ActualCanvaViewModel
                        .DesignCanvas.Children
                        .First(x => x.Name == SelectedControlName)
                    );
            SelectedControl = null;
        }

        public ControlPropertyViewModel(MainConfiguratorViewModel parentMainConfiguratorViewModel)
        {
            mainConfiguratorViewModel = parentMainConfiguratorViewModel;
            SelectedControlName = string.Empty;
            DeleteControlCommand = ReactiveCommand.Create(OnDeleteClick);
        }
        
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
