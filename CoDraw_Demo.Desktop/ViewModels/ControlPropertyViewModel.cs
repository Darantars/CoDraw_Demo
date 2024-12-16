using Avalonia.Controls;
using Avalonia.Controls.Shapes;
using Avalonia.Interactivity;
using Avalonia.Media;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class ControlPropertyViewModel : ViewModelBase
    {
        private Control selectedControl;

        //Написать поля для биндинга свойств контролов

        public void SetSelectedControl(Control control)
        {
            selectedControl = control;
            //WidthTextBox.Text = control.Width.ToString();
            //HeightTextBox.Text = control.Height.ToString();
            //ColorPicker.SelectedColor = (control as Shape)?.Fill as SolidColorBrush;
            //OpacitySlider.Value = control.Opacity;
        }

        private void OnWidthChanged(object sender, TextChangedEventArgs e)
        {
            //if (double.TryParse(WidthTextBox.Text, out double width))
            //{
            //    selectedControl.Width = width;
            //}
        }

        private void OnHeightChanged(object sender, TextChangedEventArgs e)
        {
            //if (double.TryParse(HeightTextBox.Text, out double height))
            //{
            //    selectedControl.Height = height;
            //}
        }

        private void OnColorChanged(object sender, ColorChangedEventArgs e)
        {
            //if (selectedControl is Shape shape)
            //{
            //    shape.Fill = new SolidColorBrush(ColorPicker.SelectedColor);
            //}
        }

        private void OnOpacityChanged(/*object sender, RoutedPropertyChangedEventArgs<double> e*/)
        {
            //selectedControl.Opacity = OpacitySlider.Value;
        }

        private void OnDeleteClick(object sender, RoutedEventArgs e)
        {
            //DesignCanvas.Children.Remove(selectedControl);
        }

        public ControlPropertyViewModel(MainConfiguratorViewModel mainConfiguratorViewModel) 
        { 
        
        }
    }
}
