using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Media;
using CoDraw_Demo.Desktop.ViewModels;
using DynamicData;

namespace CoDraw_Demo.Desktop.Models;

public class CanvaPointerWorker
{
    
    private CanvaViewModel  actualCanvaViewModel;
    private Point _clickPosition;
    private bool _isPointerPressed;
    private bool _isControlsMoved;
    private Control hitControl;
    public Canvas SelectionRectangleCanvas { get; set; }


    public CanvaPointerWorker(CanvaViewModel canvaViewModel)
    {
        
        actualCanvaViewModel = canvaViewModel;
        _isPointerPressed = false;
        SelectionRectangleCanvas = new Canvas
        {
            Name = "SelectionRectangleCanvas",
            IsVisible = true,
            Width = 0,
            Height = 0,
            Opacity = 0.4,
            Background = Brushes.Red
        };
    }
        
    public void OnPointerPressed(object sender, PointerPressedEventArgs e)
    {
        //1
        e.Pointer.Capture(actualCanvaViewModel.DesignCanvas);
        var position = e.GetPosition(actualCanvaViewModel.DesignCanvas);
        _isControlsMoved = false;
        _clickPosition = position;

        //2
        hitControl = actualCanvaViewModel.DesignCanvas.InputHitTest(position) as Control;
        if (hitControl == actualCanvaViewModel.DesignCanvas)
            hitControl = null;

        if (hitControl == null)
        {
            foreach (Canvas item in actualCanvaViewModel.DesignCanvas.Children)
            {
                double _xItem = Canvas.GetLeft(item);
                double _yItem = Canvas.GetTop(item);
                
                double _widthItem = item.Children.FirstOrDefault().Width;
                double _heightItem = item.Children.FirstOrDefault().Height;

                if (_xItem + 15 >= _clickPosition.X  
                    && _xItem - 15 <= _clickPosition.X + _widthItem
                    && _yItem + 15 >= _clickPosition.Y 
                    && _yItem - 15 <= _clickPosition.Y + _heightItem
                    && item != actualCanvaViewModel.DesignCanvas)
                {
                    hitControl = item.Children.FirstOrDefault() as Control;
                    actualCanvaViewModel.DraggedControls.Add(hitControl.Parent as Canvas);
                }
            }
        }

        //3.1
        if (hitControl == null)
        {
            foreach (var control in actualCanvaViewModel.DraggedControls)
            {
                control.Opacity = 1;
            }
            actualCanvaViewModel.DraggedControls.Clear();

            //4.1 - 4.2
            _isPointerPressed = true;

            //5.1
            Canvas.SetLeft(SelectionRectangleCanvas, position.X);
            Canvas.SetTop(SelectionRectangleCanvas, position.Y);
            actualCanvaViewModel.DesignCanvas.Children.Add(SelectionRectangleCanvas); //Возможно нужно через коллекцию
        }
        else
        {
            //4.1 - 4.2
            _isPointerPressed = true;

            //5.2
            if (hitControl.Parent is Canvas canvas)
            {
                // Проверяем, не выделена ли родительская канва
                if (!actualCanvaViewModel.DraggedControls.Contains(canvas))
                {
                    foreach (var control in actualCanvaViewModel.DraggedControls)
                    {
                        control.Opacity = 1;
                    }
                    actualCanvaViewModel.DraggedControls.Clear();
                }

                if (canvas != actualCanvaViewModel.DesignCanvas)
                {
                    actualCanvaViewModel.DraggedControls.Add(canvas);
                    canvas.Opacity = 0.55;
                }
                else
                {
                    actualCanvaViewModel.DraggedControls.Add(hitControl as Canvas);
                    hitControl.Opacity = 0.55;
                }

                
            }
        }
    }



    

    public void OnPointerMoved(object sender, PointerEventArgs e)
    {
        //6.1
        var hitPosition = e.GetPosition(actualCanvaViewModel.DesignCanvas);
        var hitOffset = hitPosition - _clickPosition;

        if (_isPointerPressed)
        {
            //6
            if ( actualCanvaViewModel.DesignCanvas.Children.Contains(SelectionRectangleCanvas) && actualCanvaViewModel.DraggedControls.Count == 0)
            {
                //6.2
                var rectangle = actualCanvaViewModel.DesignCanvas.Children.First(item => item.Name == "SelectionRectangleCanvas");
                
                var newWidth = Math.Abs(hitOffset.X);
                var newHeight = Math.Abs(hitOffset.Y);
                
                var newLeft = hitOffset.X < 0 ? hitPosition.X : _clickPosition.X;
                var newTop = hitOffset.Y < 0 ? hitPosition.Y : _clickPosition.Y;
                
                rectangle.Width = newWidth;
                rectangle.Height = newHeight;
                
                Canvas.SetLeft(rectangle, newLeft);
                Canvas.SetTop(rectangle, newTop);
            }
            else
            {
                //6.1
                if (actualCanvaViewModel.DraggedControls.Count() != 0)
                {
                    _isControlsMoved = true;
                    List<Canvas> motionCanvas = actualCanvaViewModel.DesignCanvas.Children
                        .OfType<Canvas>()
                        .Where(item => actualCanvaViewModel.DraggedControls.Contains(item))
                        .ToList();
                    
                    
                    
                    var Xhit = Canvas.GetLeft(hitControl.Parent);
                    var Yhit = Canvas.GetTop(hitControl.Parent);
                    
                    Canvas.SetLeft(hitControl.Parent, hitPosition.X - hitControl.Width / 2);
                    Canvas.SetTop(hitControl.Parent, hitPosition.Y - hitControl.Height / 2);
                    
                    var Xoffset = Xhit - Canvas.GetLeft(hitControl.Parent); 
                    var Yoffset = Yhit - Canvas.GetTop(hitControl.Parent); 
                    
                    foreach (var canvasItem in motionCanvas)
                    {
                        if (canvasItem == hitControl.Parent)
                            continue;

                        var X0 = Canvas.GetLeft(canvasItem);
                        var Y0 = Canvas.GetTop(canvasItem);
                        
                        Canvas.SetLeft(canvasItem, X0 - Xoffset);
                        Canvas.SetTop(canvasItem, Y0 - Yoffset);
                    } 
                }

            }
        }


    }


    public void OnPointerReleased(object sender, PointerReleasedEventArgs e)
    {

            //7.2
            _isPointerPressed = false;
            if (actualCanvaViewModel.DraggedControls.Count() != 0)
            {
                e.Pointer.Capture(null);
                if (!_isControlsMoved)
                {
                    foreach (var canvas in actualCanvaViewModel.DraggedControls)
                        canvas.Opacity = 1;
                    actualCanvaViewModel.DraggedControls.Clear();
                    Canvas hitCanvas = hitControl.Parent as Canvas;
                    if (hitCanvas != null)
                    {
                        actualCanvaViewModel.DraggedControls.Add(hitCanvas);
                        hitCanvas.Opacity = 0.55;
                    }

                }
                else
                {
                    Canvas hitCanvas = hitControl.Parent as Canvas;
                    if (hitCanvas != null)
                    {
                        actualCanvaViewModel.DraggedControls.Add(hitCanvas);
                        hitCanvas.Opacity = 0.55;
                    }
                }
            }

            //7.1
            _isPointerPressed = false;

            var selectionRectangle = actualCanvaViewModel.DesignCanvas.Children.FirstOrDefault(item => item.Name == "SelectionRectangleCanvas") as Canvas;
            if (selectionRectangle == null) return;

            var selectionX = Canvas.GetLeft(selectionRectangle);
            var selectionY = Canvas.GetTop(selectionRectangle);
            var selectionWidth = selectionRectangle.Width;
            var selectionHeight = selectionRectangle.Height;
            
            foreach (var child in actualCanvaViewModel.DesignCanvas.Children)
            {
                if (child is Canvas canvas && canvas != selectionRectangle)
                {
                    var canvasX = Canvas.GetLeft(canvas);
                    var canvasY = Canvas.GetTop(canvas);
                    var canvasWidth = canvas.Width;
                    var canvasHeight = canvas.Height;

                    if (DoRectanglesIntersect(selectionX, selectionY, selectionWidth, selectionHeight, 
                            canvasX, canvasY, canvasWidth, canvasHeight))
                    {
                        canvas.Opacity = 0.55;
                        actualCanvaViewModel.DraggedControls.Add(canvas);
                    }
                }
            }


            actualCanvaViewModel.DesignCanvas.Children.First(item => item.Name == "SelectionRectangleCanvas").Width = 0;
            actualCanvaViewModel.DesignCanvas.Children.First(item => item.Name == "SelectionRectangleCanvas").Height = 0;
            actualCanvaViewModel.DesignCanvas.Children.Remove(SelectionRectangleCanvas);
            e.Pointer.Capture(null);
        
        hitControl = null;
    }
    
    public static bool DoRectanglesIntersect(double x1, double y1, double w1, double h1, double x2, double y2, double w2, double h2)
    {
        double r1 = x1 + w1;
        double b1 = y1 + h1;
        double r2 = x2 + w2;
        double b2 = y2 + h2;
        
        return !(r1 <= x2 || r2 <= x1 || b1 <= y2 || b2 <= y1);
    }
}