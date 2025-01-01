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
        public MainConfiguratorView(MainConfiguratorViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }

        // Реализация INotifyPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}