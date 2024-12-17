using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CoDraw_Demo.Desktop.Controls;

public class BindableCanvasControl : ContentControl
{
    public static readonly DirectProperty<BindableCanvasControl, double> LeftProperty =
        AvaloniaProperty.RegisterDirect<BindableCanvasControl, double>(
            nameof(Left),
            o => o.Left,
            (o, v) => o.Left = v);

    public static readonly DirectProperty<BindableCanvasControl, double> TopProperty =
        AvaloniaProperty.RegisterDirect<BindableCanvasControl, double>(
            nameof(Top),
            o => o.Top,
            (o, v) => o.Top = v);

    private double _left;
    private double _top;

    public double Left
    {
        get => _left;
        set
        {
            SetAndRaise(LeftProperty, ref _left, value);
            Canvas.SetLeft(this, value);
        }
    }

    public double Top
    {
        get => _top;
        set
        {
            SetAndRaise(TopProperty, ref _top, value);
            Canvas.SetTop(this, value);
        }
    }

    public BindableCanvasControl()
    {

    }

}
