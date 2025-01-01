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
using CoDraw_Demo.Desktop.Models;
using ReactiveUI;
using SkiaSharp;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class ControlPropertyViewModel : ViewModelBase, INotifyPropertyChanged
    {
        private MainConfiguratorViewModel mainConfiguratorViewModel;
        private Control _selectedControl;
        private ConfiguratorObject _selectedControlObject;
        private string _selectedControlName;
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
                    var select = SelectedControl as Canvas;
                    var innerControl = select?.Children.FirstOrDefault() as Control;
                    if (innerControl != null)
                    {
                        SelectedControlObject = ConfiguratorObjectFactory.CreateConfiguratorObject(select);
                        if (HasPublicProperty(innerControl, "Fill"))
                            SelectedControlColor = (IBrush)innerControl.GetType().GetProperty("Fill").GetValue(innerControl);
                        else if (HasPublicProperty(innerControl, "Background"))
                            SelectedControlColor = (IBrush)innerControl.GetType().GetProperty("Background").GetValue(innerControl);
                        else
                            SelectedControlColor = Brushes.Transparent;
                        
                    }
                    else
                    {
                        SelectedControlObject = null;
                        SelectedControlColor = Brushes.Transparent;
                    }
                }
                else
                {
                    _selectedControl = null;
                    SelectedControlObject = null;
                    SelectedControlName = "";
                    SelectedControlColor = Brushes.Transparent;
                }
                OnPropertyChanged();
            }
        }

        public ConfiguratorObject SelectedControlObject
        {
            get => _selectedControlObject;
            set
            {
                _selectedControlObject = value;
                OnPropertyChanged();
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

        public IBrush SelectedControlColor
        {
            get => _selectedControlColor;
            set
            {
                if (SelectedControlObject == null)
                    return;
                var _innerControl = SelectedControlObject.InnerControl;
                if (HasPublicProperty(_innerControl, "Fill"))
                    _innerControl.GetType().GetProperty("Fill")?.SetValue(_innerControl, value);
                else if (HasPublicProperty(_innerControl, "Background"))
                    _innerControl.GetType().GetProperty("Background")?.SetValue(_innerControl, value);
                
                
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

