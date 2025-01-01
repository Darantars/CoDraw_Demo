using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Shapes;
using Avalonia.Input;
using Avalonia.Media;
using CoDraw_Demo.Desktop.Models;
using DynamicData;
using DynamicData.Binding;
using ReactiveUI;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class CanvaViewModel : INotifyPropertyChanged
    {
        private MainConfiguratorViewModel mainConfiguratorViewModel;
        private ObservableCollection<Canvas> _draggedControls;
        public Canvas _designCanvas;
        private ObservableCollection<CanvaControlItem> _controlsColection;
        private CanvaPointerWorker canvasPointerWorker;

        public ObservableCollection<CanvaControlItem> ControlsColection
        {
            get => _controlsColection;
            set
            {
                if (_controlsColection != value)
                {
                    _controlsColection = value;
                }
            }
        }

        public Canvas DesignCanvas
        {
            get => _designCanvas;
            set
            {
                if (_designCanvas != value)
                {
                    _designCanvas = value;
                    OnPropertyChanged();
                    InitializeCanvasEvents();
                }
            }
        }
        

        public ObservableCollection<Canvas> DraggedControls
        {
            get => _draggedControls;
            set
            {
                if (_draggedControls != value)
                {
                    if (_draggedControls != null)
                    {
                        _draggedControls.CollectionChanged -= DraggedControls_CollectionChanged;
                    }

                    _draggedControls = value;

                    if (_draggedControls != null)
                    {
                        _draggedControls.CollectionChanged += DraggedControls_CollectionChanged;
                    }

                    OnPropertyChanged();

                    if (value.Count == 1 && value.First().Children.Count == 1)
                    {
                        mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = value.First();
                    }
                    else
                    {
                        mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = null;
                    }
                }
            }
        }

        public CanvaViewModel(MainConfiguratorViewModel parentMainConfiguratorViewModel)
        {
            mainConfiguratorViewModel = parentMainConfiguratorViewModel;
            DraggedControls = new ObservableCollection<Canvas>();
            ControlsColection = new ObservableCollection<CanvaControlItem>();
            _controlsColection.CollectionChanged += ControlsColection_CollectionChanged;
            canvasPointerWorker = new CanvaPointerWorker(this);
        }

        private void ControlsColection_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (CanvaControlItem item in e.NewItems)
                {
                    Canvas newControlCanva = new Canvas
                    {
                        Height = item.width,
                        Width = item.height,
                        Name = item.Name,
                    };
                    Canvas.SetTop(newControlCanva, item.X);
                    Canvas.SetLeft(newControlCanva, item.Y);
                    newControlCanva.ZIndex = item.Z;
                    newControlCanva.Children.Add(item.control);
                    DesignCanvas.Children.Add(newControlCanva);
                }
            }

            if (e.OldItems != null)
            {
                foreach (CanvaControlItem item in e.OldItems)
                {
                    var canvasToRemove = DesignCanvas.Children.OfType<Canvas>().FirstOrDefault(c => c.Name == item.Name);
                    if (canvasToRemove != null)
                    {
                        DesignCanvas.Children.Remove(canvasToRemove);
                    }
                }
            }
        }

        private void DraggedControls_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.NewItems != null)
            {
                foreach (Canvas item in e.NewItems)
                {
                    // TODO: Обработка добавленных элементов. Хорошо бы перенести  из методов клика
                }
            }

            if (e.OldItems != null)
            {
                foreach (Canvas item in e.OldItems)
                {
                    // TODO: Обработка удаленных элементов. Хорошо бы перенести  из методов клика
                }
            }
            //TODO: Переработать участок, присутствует дубляж ссылок + реализовать редактирование свойств группы
            if (DraggedControls.Count >= 1 && DraggedControls.First().Children.Count == 1)
                mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = DraggedControls.First();
            else if (DraggedControls.Count >= 1 && DraggedControls.First().Children.Count == 1)
                mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = null;
            else
                mainConfiguratorViewModel.ActualControlsProprtyPanelViewModel.SelectedControl = null;
        }

        private async void InitializeCanvasEvents()
        {
            if (DesignCanvas != null)
            {
                DesignCanvas.PointerPressed += canvasPointerWorker.OnPointerPressed;
                DesignCanvas.PointerMoved += canvasPointerWorker.OnPointerMoved;
                DesignCanvas.PointerReleased += canvasPointerWorker.OnPointerReleased;
            }
        }
        




        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
