using System;
using PasswordBook.Contracts;
using PasswordBook.Messages;

namespace PasswordBook.Views.AddEditEntry
{
    public class AddEntryViewModelBehavior : IViewModelBehavior<AddEditEntryViewModel>
    {
        private AddEditEntryViewModel _addEntryViewModel;
        private readonly IPasswordSheetFactory _passwordSheet;
        private readonly IEventAggregator _eventAggregator;

        public AddEntryViewModelBehavior(IPasswordSheetFactory passwordSheet, IEventAggregator eventAggregator)
        {
            _passwordSheet = passwordSheet;
            _eventAggregator = eventAggregator;
        }

        public void Initialize(AddEditEntryViewModel addEntryViewModel)
        {
            _addEntryViewModel = addEntryViewModel;
            _addEntryViewModel.SaveCommand = new RelayCommand(Save, CanSave);
            _addEntryViewModel.CancelCommand = new RelayCommand(Cancel);
        }

        private void Save(object parameter)
        {
            CreateAndSave();
            CleanupAndClose();
        }

        private void CreateAndSave()
        {
            var newEntry = new PasswordEntry(Guid.NewGuid())
            {
                Title = _addEntryViewModel.Title,
                UserName = _addEntryViewModel.UserName,
                Password = _addEntryViewModel.Password,
            };

            var sheet = _passwordSheet.Get();
            sheet.Create(newEntry);
        }

        private bool CanSave(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_addEntryViewModel.UserName)
                && !String.IsNullOrWhiteSpace(_addEntryViewModel.Password)
                && !String.IsNullOrWhiteSpace(_addEntryViewModel.Title);
        }

        private void Cancel(object parameter)
        {
            CleanupAndClose();
        }

        private void CleanupAndClose()
        {
            _addEntryViewModel.IsOpen = false;
            _addEntryViewModel.Password = String.Empty;
            _addEntryViewModel.UserName = String.Empty;
            _addEntryViewModel.Title = String.Empty;
            _eventAggregator.Send(new RefreshSearch());
        }
    }
}
