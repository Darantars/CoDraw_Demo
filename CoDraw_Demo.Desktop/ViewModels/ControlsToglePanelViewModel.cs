using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Media;

namespace CoDraw_Demo.Desktop.ViewModels;
using ReactiveUI;
using System.Reactive;
public class ControlsToglePanelViewModel : ViewModelBase
{
    public ReactiveCommand<Unit, Unit> OnRectangleCommand { get; }
    public ReactiveCommand<Unit, Unit> OnEllipseCommand { get; }
    public ReactiveCommand<Unit, Unit> OnTextBlockCommand { get; }
    public MainConfiguratorViewModel ActualMainConfiguratorViewModel {get; set;}

    public ControlsToglePanelViewModel(MainConfiguratorViewModel mainConfiguratorViewModel)
    {
        ActualMainConfiguratorViewModel = mainConfiguratorViewModel;
        OnRectangleCommand = ReactiveCommand.Create(OnRectangleClick);
        OnEllipseCommand = ReactiveCommand.Create(OnEllipseClick); 
        OnTextBlockCommand = ReactiveCommand.Create(OnTextBlockClick);
    }
    
    private void OnRectangleClick()
    {
        var rectangle = new Rectangle { 
            Width = 100, 
            Height = 100, 
            Fill = Brushes.Red,
        };
        Canvas.SetLeft(rectangle, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Width /2);
        Canvas.SetTop(rectangle, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Height /2);
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(rectangle);
    }

    private void OnEllipseClick()
    {
        Control ellipse = new Ellipse {
            Width = 100, 
            Height = 100, 
            Fill = Brushes.Blue
        };
        Canvas.SetLeft(ellipse, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Width / 2);
        Canvas.SetTop(ellipse, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Height / 2);
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(ellipse);
    }

    private void OnTextBlockClick()
    {
        var textBlock = new TextBlock { 
            Width = 100, 
            Height = 30, 
            Text = "Sample Text" 
        };
        Canvas.SetLeft(textBlock, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Width / 2);
        Canvas.SetTop(textBlock, ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Height / 2);
        ActualMainConfiguratorViewModel.ActualCanvaViewModel.DesignCanvas.Children.Add(textBlock);
    }
    
}