using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using System;
using ReactiveUI;
using System.ComponentModel;
using System.Reactive;
using System.Reactive.Linq;
using System.Runtime.CompilerServices;
using Avalonia.Interactivity;
using CoDraw_Demo.Desktop.ViewModels;

namespace CoDraw_Demo.Desktop.Views.Configurator
{
    public partial class MainConfiguratorView : UserControl, INotifyPropertyChanged
    {
        public MainConfiguratorView()
        {
            DataContext = new MainConfiguratorViewModel();
            InitializeComponent();
        }
    }
}
