﻿using Avalonia.Interactivity;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reactive;
using System.Runtime.Serialization.DataContracts;
using System.Text;
using System.Threading.Tasks;

namespace CoDraw_Demo.Desktop.ViewModels
{
    public class MainConfiguratorViewModel : ViewModelBase
    {
        private bool _isRightSplitViewOpen;
        private bool _isLeftSplitViewOpen;

        public bool IsRightSplitViewOpen
        {
            get => _isRightSplitViewOpen;
            set => this.RaiseAndSetIfChanged(ref _isRightSplitViewOpen, value);
        }

        public bool IsLeftSplitViewOpen
        {
            get => _isLeftSplitViewOpen;
            set => this.RaiseAndSetIfChanged(ref _isLeftSplitViewOpen, value);
        }

        public ReactiveCommand<Unit, Unit> ChangeLeftPaneWidthCommand { get; }
        public ReactiveCommand<Unit, Unit> ChangeRightPaneWidthCommand { get; }

        public MainConfiguratorViewModel()
        {
            IsRightSplitViewOpen = true;
            IsLeftSplitViewOpen = true;
            ChangeLeftPaneWidthCommand = ReactiveCommand.Create(ChangeLeftPaneWidth);
            ChangeRightPaneWidthCommand = ReactiveCommand.Create(ChangeRightPaneWidth); 
        }

        private void ChangeLeftPaneWidth()
        {
            IsLeftSplitViewOpen = !IsLeftSplitViewOpen;
        }
        
        private void ChangeRightPaneWidth()
        {
            IsRightSplitViewOpen = !IsRightSplitViewOpen;
        }
    }
}
