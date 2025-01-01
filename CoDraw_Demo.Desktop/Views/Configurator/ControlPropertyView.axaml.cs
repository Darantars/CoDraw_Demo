using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.PropertyGrid.Controls;
using Avalonia.Skia;
using CoDraw_Demo.Desktop.ViewModels;
using SkiaSharp;

namespace CoDraw_Demo.Desktop.Views.Configurator;

public partial class ControlPropertyView : UserControl
{
    public ControlPropertyView()
    {
        var app = (App)Application.Current;
        var configuratorViewModel = app.ConfiguratorViewModel;
        DataContext = configuratorViewModel.ActualControlsProprtyPanelViewModel;
        InitializeComponent();
    }
    public void ColorPicker_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        SKColor color = e.NewColor.ToSKColor();
        (DataContext as ControlPropertyViewModel).SelectedControlColor = new SolidColorBrush(Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue)); 
    }
    
    private void OnCustomPropertyDescriptorFilter(object? sender, CustomPropertyDescriptorFilterEventArgs e)
    {
        if (e.TargetObject is Control selectedControl)
        {
            e.IsVisible = true;
            e.Handled = true;
        }
    }
}

