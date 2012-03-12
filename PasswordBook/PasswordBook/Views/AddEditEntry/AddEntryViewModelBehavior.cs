using System;
using PasswordBook.Contracts;
using PasswordBook.Messages;
using PasswordBook.Views.CommandBar;
using PasswordBook.Views.MainWindow;

namespace PasswordBook.Views.AddEditEntry
{
    public class AddEntryViewModelBehavior : IViewModelBehavior<AddEditEntryViewModel>, IViewModelBehavior<CommandBarViewModel>
    {
        private AddEditEntryViewModel _addEntryViewModel;
        private CommandBarViewModel _commandBarViewModel;
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
        }

        public void Initialize(CommandBarViewModel viewModel)
        {
            _commandBarViewModel = viewModel;
            _commandBarViewModel.AddSaveCommand = new RelayCommand(Save, CanSave);
            _commandBarViewModel.AddCancelCommand = new RelayCommand(Cancel, CanCancel);
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

        private bool CanCancel(object parameter)
        {
            if (_addEntryViewModel == null) return false;

            return _addEntryViewModel.IsOpen;
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
