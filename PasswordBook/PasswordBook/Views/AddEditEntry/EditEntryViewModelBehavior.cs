using System;
using PasswordBook.Contracts;
using PasswordBook.Messages;
using PasswordBook.Views.CommandBar;

namespace PasswordBook.Views.AddEditEntry
{
    public class EditEntryViewModelBehavior : IViewModelBehavior<AddEditEntryViewModel>, IViewModelBehavior<CommandBarViewModel>
    {
        private AddEditEntryViewModel _viewModel;
        private CommandBarViewModel _commandBarViewModel;
        private IPasswordSheetFactory _passwordSheet;
        private readonly IEventAggregator _eventAggregator;

        public EditEntryViewModelBehavior(IPasswordSheetFactory passwordSheet, IEventAggregator eventAggregator)
        {
            _passwordSheet = passwordSheet;
            _eventAggregator = eventAggregator;
        }

        public void Initialize(AddEditEntryViewModel viewModel)
        {
            _viewModel = viewModel;
        }

        public void Initialize(CommandBarViewModel viewModel)
        {
            _commandBarViewModel = viewModel;
            _commandBarViewModel.EditSaveCommand = new RelayCommand(Save, CanSave);
            _commandBarViewModel.EditCancelCommand = new RelayCommand(Cancel, CanCancel);
        }

        private void Save(object parameter)
        {
            UpdateAndSave();
            CleanupAndClose();
        }

        private void UpdateAndSave()
        {
            PasswordEntry entry = _viewModel.Item;
            entry.Title = _viewModel.Title;
            entry.UserName = _viewModel.UserName;
            entry.Password = _viewModel.Password;

            var sheet = _passwordSheet.Get();
            sheet.Update(entry.Id, entry);
        }

        private bool CanSave(object parameter)
        {
            if (_viewModel == null) return false;

            return !String.IsNullOrWhiteSpace(_viewModel.UserName)
                && !String.IsNullOrWhiteSpace(_viewModel.Password)
                && !String.IsNullOrWhiteSpace(_viewModel.Title);
        }

        private bool CanCancel(object parameter)
        {
            if (_viewModel == null) return false;

            return _viewModel.IsOpen;
        }

        private void Cancel(object parameter)
        {
            CleanupAndClose();
        }

        private void CleanupAndClose()
        {
            _viewModel.IsOpen = false;
            _viewModel.Password = String.Empty;
            _viewModel.UserName = String.Empty;
            _viewModel.Title = String.Empty;
            _eventAggregator.Send(new RefreshSearch());
        }
    }
}
