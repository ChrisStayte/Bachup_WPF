﻿using Bachup.Helpers;
using Bachup.Model;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bachup.ViewModel
{
    class EditBachupItemViewModel : BaseViewModel
    {
        public EditBachupItemViewModel(BachupItem bi)
        {
            CancelCommand = new RelayCommand(Cancel);
            SaveCommand = new RelayCommand(Save);

            ShowMessage = false;
            Message = "";
            _bachupItem = bi;
            Name = bi.Name;
        }

        private readonly BachupItem _bachupItem;

        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                NotifyPropertyChanged();
            }
        }

        private string _message;
        public string Message
        {
            get
            {
                return _message;
            }
            set
            {
                _message = value;
                NotifyPropertyChanged();
            }
        }

        private bool _showMessage;
        public bool ShowMessage
        {
            get { return _showMessage; }
            set
            {
                _showMessage = value;
                NotifyPropertyChanged();
            }
        }

        // Relay Commands

        public RelayCommand CancelCommand { get; private set; }
        public RelayCommand SaveCommand { get; private set; }

        #region Events

        private void Cancel(object parameter)
        {
            DialogHost.CloseDialogCommand.Execute(null, null);
        }

        private void Save(object parameter)
        {
            if (CheckRequirements())
            {
                _bachupItem.Name = Name;
                DialogHost.CloseDialogCommand.Execute(null, null);
            }
        }

        private bool CheckRequirements()
        {
            if (_name == null)
            {
                Message = "Enter a Name";
                ShowMessage = true;
                return false;
            }

            ShowMessage = false;

            return true;
        }

        #endregion

    }


}