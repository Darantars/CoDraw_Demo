using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using DynamicData;
using System.Linq;
using DynamicData.Binding;
using System.Threading.Tasks;
using ReactiveUI;
using System.Collections.Specialized;
using CoDraw_Demo.Desktop.Models;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private Canvas hightLitedControl;
        private Point clickPosition;
        public Canvas designCanvas;
        
        
        private ObservableCollection<CanvaControlItem> controlsColection;

        public ObservableCollection<CanvaControlItem> ControlsColection
        {
            get => controlsColection;
            set
            {
                if (controlsColection != value)
                {
                    controlsColection = value;
                }
            }
        }

        public Canvas DesignCanvas
        {
            get => designCanvas;
            set
            {
                if (designCanvas != value)
                {
                    designCanvas = value;
                    OnPropertyChanged();
                    InitializeCanvasEvents();
                }
            }
        }

        public CanvaViewModel()
        {

            ControlsColection = new ObservableCollection<CanvaControlItem>();
            controlsColection.CollectionChanged += ControlsColection_CollectionChanged;
        }

        private void ControlsColection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CanvaControlItem item in e.NewItems)
                {
                    Canvas newControlCanva = new Canvas
                    {
                        Height = item.width,
                        Width = item.height,
                        Name = item.name,
                        
                    };
                    Canvas.SetTop(newControlCanva, item.X);
                    Canvas.SetLeft(newControlCanva, item.y);
                    newControlCanva.Children.Add(item.control);
                    DesignCanvas.Children.Add(newControlCanva);
                }
            }

            if (e.OldItems != null)
            {
                foreach (CanvaControlItem item in e.OldItems)
                {
                    var canvasToRemove = DesignCanvas.Children.OfType<Canvas>().FirstOrDefault(c => c.Name == item.name);
                    if (canvasToRemove != null)
                    {
                        DesignCanvas.Children.Remove(canvasToRemove);
                    }
                }
            }
        }

        private async void InitializeCanvasEvents()
        {
            if (DesignCanvas != null)
            {
                DesignCanvas.PointerPressed += OnPointerPressed;
                DesignCanvas.PointerMoved += OnPointerMoved;
                DesignCanvas.PointerReleased += OnPointerReleased;
            }
        }

        private void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            var position = e.GetPosition(DesignCanvas);
            var hitControl = DesignCanvas.InputHitTest(position) as Control;

            if (hitControl != null)
            {
                if(hitControl != null)
                    hightLitedControl = (Canvas)hitControl.Parent;
                if (hightLitedControl != null)
                {
                    clickPosition = position;
                    e.Pointer.Capture(DesignCanvas);
                }
            }
        }

        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (hightLitedControl != null)
            {
                var position = e.GetPosition(DesignCanvas);
                var offset = position - clickPosition;
                Canvas motionCanvas = DesignCanvas.Children.First(item => item == hightLitedControl) as Canvas;
                Canvas.SetLeft(motionCanvas, position.X + offset.X);
                Canvas.SetTop(motionCanvas, position.Y + offset.Y);
            }
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (hightLitedControl != null)
            {
                e.Pointer.Capture(null);
            }
            hightLitedControl = null;
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
