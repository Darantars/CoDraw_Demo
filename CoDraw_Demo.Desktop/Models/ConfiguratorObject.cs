using System;
using Avalonia.Controls;
using Avalonia.Media;
using ReactiveUI;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using Avalonia.Controls.Shapes;

namespace CoDraw_Demo.Desktop.Models
{


    public class ConfiguratorObject : ReactiveObject, INotifyPropertyChanged
    {
        private Control _canva;
        private Control _innerControl;
        private string _name;
        private double _x;
        private double _y;
        private int _z;
        private double _opacity;
        private IBrush _color;

        public Control Canva
        {
            get => _canva;
            set
            {
                if (value != null)
                {
                    _canva = value;
                    Name = value.Name;
                    X = Canvas.GetLeft(value);
                    Y = Canvas.GetTop(value);
                    Z = value.ZIndex;
                    var select = Canva as Canvas;
                    InnerControl = select?.Children.FirstOrDefault() as Control;
                    if (InnerControl != null)
                    {
                        Opacity = InnerControl.Opacity;
                    }
                    else
                    {
                        Opacity = 1;
                    }
                }
                else
                {
                    _canva = null;
                    Name = "";
                    X = 0;
                    Y = 0;
                    Z = 0;
                    Opacity = 1;
                }
                OnPropertyChanged();
            }
        }

        public Control InnerControl
        {
            get => _innerControl;
            set
            {
                _innerControl = value;
                OnPropertyChanged();
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public double X
        {
            get => _x;
            set
            {
                _x = value;
                if (Canva != null)
                    Canvas.SetLeft(Canva, value);
                OnPropertyChanged();
            }
        }

        public double Y
        {
            get => _y;
            set
            {
                _y = value;
                if (Canva != null)
                    Canvas.SetTop(Canva, value);

                OnPropertyChanged();
            }
        }

        public int Z
        {
            get => _z;
            set
            {
                if (Canva != null)
                    Canva.ZIndex = value;
                _z = value;
                OnPropertyChanged();
            }
        }

        public double Opacity
        {
            get
            {
                if (Canva != null)
                {
                    var select = Canva as Canvas;
                    return select?.Children.FirstOrDefault()?.Opacity ?? 0;
                }
                else
                    return 0;
            }
            set
            {
                if (Canva != null)
                {
                    var select = Canva as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.OpacityProperty, value);
                }
                _opacity = value;
                OnPropertyChanged();
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public static bool HasPublicProperty(object obj, string propertyName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(nameof(obj));
            }

            Type type = obj.GetType();
            PropertyInfo propertyInfo = type.GetProperty(propertyName);
            return propertyInfo != null && propertyInfo.CanRead;
        }
    }

    public interface IHasWidthHeight
    {
        double Width { get; set; }
        double Height { get; set; }
    }

    public interface IHasText
    {
        string Text { get; set; }
        IBrush Foreground { get; set; }
    }

    public interface IHasContent
    {
        object Content { get; set; }
    }
    
    public interface IHasColor
    {
        IBrush Color { get; set; }
    }

    public class SimpleGeometryStrategy : ConfiguratorObject, IHasWidthHeight, IHasColor
    {
        private double _width;
        private double _height;
        private IBrush _color;

        public double Width
        {
            get => _width;
            set
            {
                if (Canva != null)
                {
                    Canva.Width = value;
                    var select = Canva as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.WidthProperty, value);
                }
                _width = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (Canva != null)
                {
                    Canva.Height = value;
                    var select = Canva as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.HeightProperty, value);
                }
                _height = value;
                OnPropertyChanged();
            }
        }
        
        public IBrush Color
        {
            get => _color;
            set
            {
                if (Canva != null && Canva is Canvas canvas)
                {
                    if (canvas.Children.First() is Control control)
                    {
                        if (HasPublicProperty(control, "Fill"))
                            control.GetType().GetProperty("Fill")?.SetValue(control, value);
                        else if (HasPublicProperty(control, "Background"))
                            control.GetType().GetProperty("Background")?.SetValue(control, value);
                    }
                }
                _color = value;
                OnPropertyChanged();
            }
        }

        public SimpleGeometryStrategy(Canvas canvas)
        {
            InnerControl = canvas.Children.FirstOrDefault();
            if (InnerControl as Control != null)
            {
                Width = InnerControl.Width;
                Height = InnerControl.Height;
                if (HasPublicProperty(InnerControl, "Fill"))
                    Color = (IBrush)InnerControl.GetType().GetProperty("Fill").GetValue(InnerControl);
                else if (HasPublicProperty(InnerControl, "Background"))
                    Color = (IBrush)InnerControl.GetType().GetProperty("Background").GetValue(InnerControl);
                else
                    Color = Brushes.Transparent;
            }
        }
    }
    
    

    public class TextBlockStrategy : ConfiguratorObject, IHasText, IHasColor
    {
        private string _text;
        private IBrush _foreground;
        private IBrush _color;
        public IBrush Color
        {
            get => _color;
            set
            {
                if (Canva != null && Canva is Canvas canvas)
                {
                    if (canvas.Children.First() is Control control)
                    {
                        if (HasPublicProperty(control, "Fill"))
                            control.GetType().GetProperty("Fill")?.SetValue(control, value);
                        else if (HasPublicProperty(control, "Background"))
                            control.GetType().GetProperty("Background")?.SetValue(control, value);
                    }
                }
                _color = value;
                OnPropertyChanged();
            }
        }

        public string Text
        {
            get => _text;
            set
            {
                if (InnerControl != null && InnerControl is TextBlock textBlock)
                {
                    textBlock.Text = value;
                }
                _text = value;
                OnPropertyChanged();
            }
        }

        public IBrush Foreground
        {
            get => _foreground;
            set
            {
                if (InnerControl != null && InnerControl is TextBlock textBlock)
                {
                    textBlock.Foreground = value;
                }
                _foreground = value;
                OnPropertyChanged();
            }
        }

        public TextBlockStrategy(Canvas canvas)
        {
            InnerControl = canvas.Children.FirstOrDefault() as Control;
            if (InnerControl is TextBlock textBlock)
            {
                Foreground = textBlock.Foreground;
                Text = textBlock.Text;
                if (HasPublicProperty(InnerControl, "Fill"))
                    Color = (IBrush)InnerControl.GetType().GetProperty("Fill").GetValue(InnerControl);
                else if (HasPublicProperty(InnerControl, "Background"))
                {
                    var _contrColor = InnerControl.GetType().GetProperty("Background").GetValue(InnerControl) as IBrush;
                    if (_contrColor == null)
                        Color = Brushes.Transparent;
                    else
                    {
                        Color = _contrColor;
                    }
                }
                else
                    Color = Brushes.Transparent;
            }
        }
    }

    public class ButtonStrategy : ConfiguratorObject, IHasWidthHeight, IHasColor
    {
        private double _width;
        private double _height;
        private IBrush _color;

        public double Width
        {
            get => _width;
            set
            {
                if (Canva != null)
                {
                    Canva.Width = value;
                    var select = Canva as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.WidthProperty, value);
                }
                _width = value;
                OnPropertyChanged();
            }
        }
        
        
        public IBrush Color
        {
            get => _color;
            set
            {
                if (Canva != null && Canva is Canvas canvas)
                {
                    if (canvas.Children.First() is Control control)
                    {
                        if (HasPublicProperty(control, "Fill"))
                            control.GetType().GetProperty("Fill")?.SetValue(control, value);
                        else if (HasPublicProperty(control, "Background"))
                            control.GetType().GetProperty("Background")?.SetValue(control, value);
                    }
                }
                _color = value;
                OnPropertyChanged();
            }
        }

        public double Height
        {
            get => _height;
            set
            {
                if (Canva != null)
                {
                    Canva.Height = value;
                    var select = Canva as Canvas;
                    select?.Children.FirstOrDefault()?.SetValue(Control.HeightProperty, value);
                }
                _height = value;
                OnPropertyChanged();
            }
        }

        public ButtonStrategy(Canvas canvas)
        {
            InnerControl = canvas.Children.FirstOrDefault();
            if (InnerControl as Button != null)
            {
                Width = InnerControl.Width;
                Height = InnerControl.Height;
                if (HasPublicProperty(InnerControl, "Fill"))
                    Color = (IBrush)InnerControl.GetType().GetProperty("Fill").GetValue(InnerControl);
                else if (HasPublicProperty(InnerControl, "Background"))
                    Color = (IBrush)InnerControl.GetType().GetProperty("Background").GetValue(InnerControl);
                else
                    Color = Brushes.Transparent;
            }
        }
    }

    public class ConfiguratorObjectFactory
    {
        public static ConfiguratorObject CreateConfiguratorObject(Canvas canvas)
        {
            var _innerControl = canvas.Children.FirstOrDefault();
            if (_innerControl as Control == null)
                return null;
            if (_innerControl is Rectangle || _innerControl is Ellipse)
            {
                return new SimpleGeometryStrategy(canvas) { Canva = canvas };
            }
            else if (_innerControl is TextBlock)
            {
                return new TextBlockStrategy(canvas) { Canva = canvas };
            }
            else if (_innerControl is Button)
            {
                return new ButtonStrategy(canvas) { Canva = canvas };
            }
            else
            {
                return new ConfiguratorObject { Canva = canvas };
            }
        }
    }
}
