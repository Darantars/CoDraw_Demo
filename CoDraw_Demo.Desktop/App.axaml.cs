using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Markup.Xaml;
using CoDraw_Demo.Desktop.ViewModels;
using CoDraw_Demo.Desktop.Views;
using CoDraw_Demo.Desktop.Views.Configurator;

namespace CoDraw_Demo.Desktop
{
    public partial class App : Application
    {
        public MainConfiguratorViewModel ConfiguratorViewModel { get; set; }

        public override void Initialize()
        {
            AvaloniaXamlLoader.Load(this);
        }

        public override void OnFrameworkInitializationCompleted()
        {
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow = new MainWindow
                {
                    DataContext = new MainViewModel()
                };
            }
            else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
            {
                singleViewPlatform.MainView = new MainView
                {
                    DataContext = new MainViewModel()
                };
            }

            base.OnFrameworkInitializationCompleted();
            AdditionalInitialization();
        }

        private void AdditionalInitialization()
        {
            ConfiguratorViewModel = new MainConfiguratorViewModel();

            // Создание экземпляра MainConfiguratorView и передача ViewModel
            var configuratorView = new MainConfiguratorView(ConfiguratorViewModel);
            // Добавление configuratorView в ваше приложение, например, в MainWindow
            if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
            {
                desktop.MainWindow.Content = configuratorView;
            }
        }
    }
}