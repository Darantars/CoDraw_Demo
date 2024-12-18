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
using System;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private MainConfiguratorViewModel mainConfiguratorViewModel;
        private Canvas _draggedControl;
        private Point clickPosition;
        public Canvas designCanvas;
        private bool isPointerPressed;
        
        
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
                    Test500();
                }
            }
        }

        public Canvas DraggedControl
        {
            get => _draggedControl;
            set
            {
                if (_draggedControl != value)
                {
                    _draggedControl = value;
                    OnPropertyChanged();
                    if (value != null && value.Children != null)
                    {
                        mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = value;
                    }
                    else
                    {
                        mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = null;
                    }
                }
            }
        }

        public CanvaViewModel(MainConfiguratorViewModel parentMainConfiguratorViewModel )
        {
            mainConfiguratorViewModel = parentMainConfiguratorViewModel;
            isPointerPressed = false;
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
                        Name = item.Name,
                        
                    };
                    Canvas.SetTop(newControlCanva, item.X);
                    Canvas.SetLeft(newControlCanva, item.Y);
                    newControlCanva.ZIndex = item.Z;   
                    newControlCanva.Children.Add(item.control);
                    DesignCanvas.Children.Add(newControlCanva);
                }
            }

            if (e.OldItems != null)
            {
                foreach (CanvaControlItem item in e.OldItems)
                {
                    var canvasToRemove = DesignCanvas.Children.OfType<Canvas>().FirstOrDefault(c => c.Name == item.Name);
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
            if (hitControl == null)
            {
                DraggedControl = null;
                isPointerPressed = false;
                return;
            }
            if (DraggedControl != null)
            {
                DraggedControl.Opacity = 1;
                DraggedControl = null;
            }
            DraggedControl = hitControl.Parent as Canvas;
            if (DraggedControl == null)
                return;
            clickPosition = position;
            e.Pointer.Capture(DesignCanvas);
            DraggedControl.Opacity = 0.75;
            isPointerPressed = true;

        }

        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (DraggedControl != null && isPointerPressed)
            {
                var position = e.GetPosition(DesignCanvas);
                var offset = position - clickPosition;
                Canvas motionCanvas = DesignCanvas.Children.First(item => item == DraggedControl) as Canvas;
                Canvas.SetLeft(motionCanvas, position.X - motionCanvas.Width / 2);
                Canvas.SetTop(motionCanvas, position.Y - motionCanvas.Height / 2);
            }
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            isPointerPressed = false;
            if (DraggedControl != null)
            {
                e.Pointer.Capture(null);

            }
            
        }


        //Test
        public void Test500()
        {
            for (int i = 0; i < 500; i++)
            {
                Canvas newControlCanva = new Canvas
                {
                    Height = 10,
                    Width = 10,
                    Name = Guid.NewGuid().ToString(),

                };
                Canvas.SetTop(newControlCanva, 555);
                Canvas.SetLeft(newControlCanva, 555);
                newControlCanva.Children.Add( new Rectangle() { Width = 10, Height = 10, Fill = Avalonia.Media.Brush.Parse("Red") });
                DesignCanvas.Children.Add(newControlCanva);
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        
    }
}
