using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoDraw_Demo.Desktop.Contracts;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private Control draggedControl;
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
                    OnPropertyChanged(nameof(ControlsColection));
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
            Control ellipse = new Ellipse
            {
                Width = 100,
                Height = 100,
                Fill = Brushes.Blue
            };
            double x = 500;
            double y = 500;
            ControlsColection = new ObservableCollection<CanvaControlItem>() { new CanvaControlItem(x, y, 1, ellipse) };
        }

        private void InitializeCanvasEvents()
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
            draggedControl = DesignCanvas.InputHitTest(position) as Control;
            if (draggedControl != null)
            {
                clickPosition = position;
                e.Pointer.Capture(DesignCanvas); 
            }
        }

        private void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (draggedControl != null/* && e.Pointer.Captured == DesignCanvas*/)
            {
                var position = e.GetPosition(DesignCanvas);
                var offset = position - clickPosition;
                Canvas.SetLeft(draggedControl, Canvas.GetLeft(draggedControl) + offset.X);
                Canvas.SetTop(draggedControl, Canvas.GetTop(draggedControl) + offset.Y);
            }
        }

        private void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (draggedControl != null)
            {
                e.Pointer.Capture(null); // Corrected line
                draggedControl = null;
            }
        }



        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
