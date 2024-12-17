using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;
using CoDraw_Demo.Desktop.Contracts;

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
            Width = 100,
            Height = 100,
            Fill = Brushes.Red,
        };
        double x = 500;
        double y = 500;
        /*ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(rectangle);*/
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, rectangle));
    }

    private void OnEllipseClick()
    {
        Control ellipse = new Ellipse
        {
            Width = 100,
            Height = 100,
            Fill = Brushes.Blue
        };
        double x = 500;
        double y = 500;
        //ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(ellipse);
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, ellipse));
    }

    private void OnTextBlockClick()
    {
        Control textBlock = new TextBlock
        {
            Width = 100,
            Height = 30,
            Text = "Sample Text"
        };
        double x = 500;
        double y = 500;
        //ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(textBlock);
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.ControlsColection.Add(new CanvaControlItem(x, y, 1, textBlock));
    }

}