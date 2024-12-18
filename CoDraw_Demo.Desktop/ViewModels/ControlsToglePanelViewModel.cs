using System;
using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CoDraw_Demo.Desktop.Models;

namespace CoDraw_Demo.Desktop.ViewModels;
using ReactiveUI;

using System.Reactive;
public class ControlsToglePanelViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> OnRectangleCommand { get; }
    public ReactiveCommand<Unit, Unit> OnEllipseCommand { get; }
    public ReactiveCommand<Unit, Unit> OnTextBlockCommand { get; }
    public MainConfiguratorViewModel ActualMainConfiguratorViewModel { get; set; }

    public ControlsToglePanelViewModel(MainConfiguratorViewModel mainConfiguratorViewModel)
    {
        ActualMainConfiguratorViewModel = mainConfiguratorViewModel;
        OnRectangleCommand = ReactiveCommand.Create(OnRectangleClick);
        OnEllipseCommand = ReactiveCommand.Create(OnEllipseClick);
        OnTextBlockCommand = ReactiveCommand.Create(OnTextBlockClick);
    }

    private void OnRectangleClick()
    {
        Control rectangle = new Rectangle
        {
            Name = "Rectangle"+" "+Guid.NewGuid().ToString(),
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
            Name = "Ellipse"+" "+Guid.NewGuid().ToString(),
            Width = 100,
            Height = 100,
            Fill = Brushes.Blue
        };
        double x = 500;
        double y = 400;
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, ellipse));
    }

    private void OnTextBlockClick()
    {
        Control textBlock = new TextBlock
        {
            Name = "TextBlock"+" "+Guid.NewGuid().ToString(),
            Width = 100,
            Height = 30,
            Text = "Sample Text"
        };
        double x = 500;
        double y = 400;
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, textBlock));
    }

}