using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CoDraw_Demo.Desktop.Models;
using ReactiveUI;
using System.Reactive;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class ControlsToglePanelViewModel : ViewModelBase
    {
        public ReactiveCommand<Unit, Unit> OnRectangleCommand { get; }
        public ReactiveCommand<Unit, Unit> OnEllipseCommand { get; }
        public ReactiveCommand<Unit, Unit> OnPathCommand { get; }
        public ReactiveCommand<Unit, Unit> OnTextBlockCommand { get; }
        public ReactiveCommand<Unit, Unit> OnButtonCommand { get; }

        public MainConfiguratorViewModel ActualMainConfiguratorViewModel { get; set; }

        public ControlsToglePanelViewModel(MainConfiguratorViewModel mainConfiguratorViewModel)
        {
            ActualMainConfiguratorViewModel = mainConfiguratorViewModel;
            OnRectangleCommand = ReactiveCommand.Create(OnRectangleClick);
            OnEllipseCommand = ReactiveCommand.Create(OnEllipseClick);
            OnPathCommand = ReactiveCommand.Create(OnPathClick);
            OnTextBlockCommand = ReactiveCommand.Create(OnTextBlockClick);
            OnButtonCommand = ReactiveCommand.Create(OnButtonClick);
        }

        private void OnRectangleClick()
        {
            Control rectangle = new Rectangle
            {
                Name = "Rectangle" + " " + Guid.NewGuid().ToString(),
                Width = 100,
                Height = 100,
                Fill = Brushes.Red,
            };
            double x = 500;
            double y = 400;
            ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, rectangle));
        }

        private void OnEllipseClick()
        {
            Control ellipse = new Ellipse
            {
                Name = "Ellipse" + " " + Guid.NewGuid().ToString(),
                Width = 100,
                Height = 100,
                Fill = Brushes.Blue
            };
            double x = 500;
            double y = 400;
            ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, ellipse));
        }
        
        
        private void OnPathClick()
        {
            Control path = new Path
            {
                Name = "Path" + " " + Guid.NewGuid().ToString(),
                Data = Geometry.Parse("M0,0 L100,0 L50,100 Z"),
                Fill = Brushes.Purple
            };
            double x = 500;
            double y = 400;
            ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, path));
        }

        private void OnTextBlockClick()
        {
            Control textBlock = new TextBlock
            {
                Name = "TextBlock" + " " + Guid.NewGuid().ToString(),
                Text = "Кнопка",
                Foreground = Brushes.Black,
                FontSize = 16,
                Width = 100,
                Height = 30,
                FlowDirection = FlowDirection.LeftToRight
            };
            double x = 500;
            double y = 400;
            ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, textBlock));
        }

        private void OnButtonClick()
        {
            Control Button = new Button
            {
                Name = "Button" + " " + Guid.NewGuid().ToString(),
                Width = 100,
                Height = 30,
                IsHitTestVisible = false,
                Content = "STOP"
            };
            double x = 500;
            double y = 400;
            ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, Button));
        }
    }
}
