using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook
{
    public class AddEntryViewModelLogic
    {
        private AddEntryViewModel _addEntryViewModel;

        public AddEntryViewModelLogic(AddEntryViewModel addEntryViewModel)
        {
            _addEntryViewModel = addEntryViewModel;
            _addEntryViewModel.SaveCommand = new RelayCommand(Save, CanSave);
            _addEntryViewModel.CancelCommand = new RelayCommand(Cancel);
        }

        private void Save(object parameter)
        {
        }

        private bool CanSave(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_addEntryViewModel.UserName)
                && !String.IsNullOrWhiteSpace(_addEntryViewModel.Password);
        }

        private void Cancel(object parameter)
        {
            _addEntryViewModel.IsOpen = false;
        }
    }
}
