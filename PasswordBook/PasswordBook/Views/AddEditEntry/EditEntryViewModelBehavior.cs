﻿using System;
using PasswordBook.Contracts;

namespace PasswordBook.Views.AddEditEntry
{
    public class EditEntryViewModelBehavior : IViewModelBehavior<AddEditEntryViewModel>
    {
        private AddEditEntryViewModel _viewModel;
        private IPasswordSheetFactory _passwordSheet;

        public EditEntryViewModelBehavior(IPasswordSheetFactory passwordSheet)
        {
            _passwordSheet = passwordSheet;
        }

        public void Initialize(AddEditEntryViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.SaveCommand = new RelayCommand(Save, CanSave);
            _viewModel.CancelCommand = new RelayCommand(Cancel);
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
            return !String.IsNullOrWhiteSpace(_viewModel.UserName)
                && !String.IsNullOrWhiteSpace(_viewModel.Password)
                && !String.IsNullOrWhiteSpace(_viewModel.Title);
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
        }
    }
}
