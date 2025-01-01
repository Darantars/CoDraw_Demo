using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CoDraw_Demo.Desktop.ViewModels;

namespace CoDraw_Demo.Desktop.Views.Configurator
{
    public partial class CanvaView : UserControl
    {
        public CanvaView()
        {
            InitializeComponent();
            this.Loaded += CanvaView_Loaded;
        }

        private void CanvaView_Loaded(object sender, Avalonia.Interactivity.RoutedEventArgs e)
        {
            var app = (App)Application.Current;
            var configuratorViewModel = app.ConfiguratorViewModel;
            DataContext = configuratorViewModel.ActualCanvaViewModel;

            var designCanvas = this.FindControl<Canvas>("DesignCanvas");
            (DataContext as CanvaViewModel).DesignCanvas = designCanvas;
        }

        private void InitializeComponent()
        {
            AvaloniaXamlLoader.Load(this);
        }
    }
}
