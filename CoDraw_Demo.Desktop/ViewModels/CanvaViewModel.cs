using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Controls.ApplicationLifetimes;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private Control draggedControl;
        private Point clickPosition;
        public Canvas designCanvas;

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
