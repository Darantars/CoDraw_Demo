using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using Avalonia.Media;
using Avalonia.Skia;
using CoDraw_Demo.Desktop.ViewModels;
using SkiaSharp;

namespace CoDraw_Demo.Desktop.Views.Configurator;

public partial class ControlPropertyView : UserControl
{
    public ControlPropertyView()
    {
        InitializeComponent();
    }
    public void ColorPicker_OnColorChanged(object? sender, ColorChangedEventArgs e)
    {
        SKColor color = e.NewColor.ToSKColor();
        (DataContext as ControlPropertyViewModel).SelectedControlColor = new SolidColorBrush(Color.FromArgb(color.Alpha, color.Red, color.Green, color.Blue)); 
    }
}