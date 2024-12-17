using System.Collections.ObjectModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using CoDraw_Demo.Desktop.Contracts;
using System.Linq;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private CanvaControlItem draggedControlItem;
        private Canvas _designCanvas;
        private Point clickPosition;

        public Canvas DesignCanvas
        {
            get => _designCanvas;
            set
            {
                if (_designCanvas != value)
                {
                    _designCanvas = value;
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
                DesignCanvas.PointerPressed += OnPointerPressed!;
                DesignCanvas.PointerReleased += OnPointerReleased!;
                DesignCanvas.PointerMoved += OnPointerMoved!;
                DesignCanvas.PointerCaptureLost += OnPointerCaptureLost!;
            }
        }

        public void OnPointerPressed(object sender, PointerPressedEventArgs e)
        {
            var position = e.GetPosition(DesignCanvas);
            var control = DesignCanvas.InputHitTest(position) as Control;
            if (control != null)
            {
                draggedControlItem = DesignCanvas.Children
                    .OfType<CanvaControlItem>()
                    .FirstOrDefault(item => item.Control == control);
                if (draggedControlItem != null)
                {
                    clickPosition = position;
                    e.Pointer.Capture(DesignCanvas);
                }
            }
        }

        public void OnPointerMoved(object sender, PointerEventArgs e)
        {
            if (draggedControlItem != null && e.Pointer.Captured == DesignCanvas)
            {
                var position = e.GetPosition(DesignCanvas);
                var offset = position - clickPosition;
                draggedControlItem.X += offset.X;
                draggedControlItem.Y += offset.Y;
                
                Canvas.SetLeft(draggedControlItem.Control, draggedControlItem.X);
                Canvas.SetTop(draggedControlItem.Control, draggedControlItem.Y);
                
                clickPosition = position;
            }
        }

        public void OnPointerReleased(object sender, PointerReleasedEventArgs e)
        {
            if (draggedControlItem != null && e.Pointer.Captured == DesignCanvas)
            {
                e.Pointer.Capture(null);
                draggedControlItem = null;
            }
        }

        public void OnPointerCaptureLost(object sender, PointerCaptureLostEventArgs e)
        {
            if (draggedControlItem != null)
            {
                draggedControlItem = null;
            }
        }

        public void AddControl(CanvaControlItem control)
        {
            DesignCanvas.Children.Add(control.Control);
            Canvas.SetLeft(control.Control, control.X);
            Canvas.SetTop(control.Control, control.Y);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
