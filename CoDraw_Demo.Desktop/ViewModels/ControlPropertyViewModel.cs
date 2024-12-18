using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia.Skia;
using ReactiveUI;
using SkiaSharp;

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
        private IBrush _selectedControlColor;

        public Control SelectedControl
        {
            get => _selectedControl;
            set
            {
                if (value != null)
                {
                    _selectedControl = value;
                    SelectedControlName = value.Name;
                    SelectedControlWidth = value.Width;
                    SelectedControlHeight = value.Height;
                    SelectedControlX = Canvas.GetLeft(value);
                    SelectedControlY = Canvas.GetTop(value);
                    SelectedControlZ = value.ZIndex;
                    var select = SelectedControl as Canvas;
                    var innerControl = select?.Children.FirstOrDefault() as Control;
                    if (innerControl != null)
                    {
                        SelectedControlOpacity = innerControl.Opacity;
                        if (HasPublicProperty(innerControl, "Fill"))
                            SelectedControlColor = (IBrush)innerControl.GetType().GetProperty("Fill").GetValue(innerControl);
                        else if (HasPublicProperty(innerControl, "Background"))
                            SelectedControlColor = (IBrush)innerControl.GetType().GetProperty("Background").GetValue(innerControl);
                        else
                            SelectedControlColor = Brushes.Transparent;
                    }
                    else
                    {
                        SelectedControlOpacity = 1;
                        SelectedControlColor = Brushes.Transparent;
                    }
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
                    SelectedControlOpacity = 1;
                    SelectedControlColor = Brushes.Transparent;
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
            get => _selectedControlWidth;
            set
            {
                if (SelectedControl != null)
                {
                    SelectedControl.Width = value;
                    var select = SelectedControl as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.WidthProperty, value);
                }
                _selectedControlWidth = value;
                OnPropertyChanged();
            }
        }

        public double SelectedControlHeight
        {
            get => _selectedControlHeight;
            set
            {
                if (SelectedControl != null)
                {
                    SelectedControl.Height = value;
                    var select = SelectedControl as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.HeightProperty, value);
                }
                _selectedControlHeight = value;
                OnPropertyChanged();
            }
        }

        public double SelectedControlX
        {
            get => _selectedControlX;
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
            get => _selectedControlY;
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
            get => _selectedControlZ;
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
                if (SelectedControl != null)
                {
                    var select = SelectedControl as Canvas;
                    return select?.Children.FirstOrDefault()?.Opacity ?? 0;
                }
                else
                    return 0;
            }
            set
            {
                if (SelectedControl != null)
                {
                    var select = SelectedControl as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.OpacityProperty, value);
                }
                _selectedControlOpacity = value;
                OnPropertyChanged();
            }
        }

        public IBrush SelectedControlColor
        {
            get
            {
                return _selectedControlColor;
            }
            set
            {
                if (SelectedControl != null && SelectedControl is Canvas canvas)
                {
                    if(canvas.Children.First() is Control control)
                    {
                        if (HasPublicProperty(control, "Fill"))
                            control.GetType().GetProperty("Fill")?.SetValue(control, value);
                        else if (HasPublicProperty(control, "Background"))
                            control.GetType().GetProperty("Background")?.SetValue(control, value);
                    }
                }
                _selectedControlColor = value;
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
        

        public static bool HasPublicProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo != null && propertyInfo.CanRead;
        }
    }
}
