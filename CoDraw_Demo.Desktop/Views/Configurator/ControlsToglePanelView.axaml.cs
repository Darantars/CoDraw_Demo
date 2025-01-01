using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoDraw_Demo.Desktop.ViewModels;

namespace CoDraw_Demo.Desktop.Views.Configurator;

public partial class ControlsToglePanelView : UserControl
{
    public ControlsToglePanelView()
    {
        var app = (App)Application.Current;
        var configuratorViewModel = app.ConfiguratorViewModel;
        DataContext = configuratorViewModel.ActualControlsToglePanelViewModel;
        InitializeComponent();
    }
}