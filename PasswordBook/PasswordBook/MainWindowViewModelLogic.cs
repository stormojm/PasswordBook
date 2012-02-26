using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using PasswordBook.Model;
using PasswordBook.Contracts;

namespace PasswordBook
{
    public class MainWindowViewModelLogic : IViewModelBehavior<MainWindowViewModel>
    {
        private MainWindowViewModel _viewModel;
        private IPasswordSheetFactory _passwordSheetFactory;

        public MainWindowViewModelLogic(IPasswordSheetFactory passwordSheetFactory)
        {
            _passwordSheetFactory = passwordSheetFactory;
        }

        public void Initialize(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.SearchCommand = new RelayCommand(Search);
            _viewModel.AddCommand = new RelayCommand(ShowAddPanel, obj => !_viewModel.AddEntryViewModel.IsOpen);
            _viewModel.EditCommand = new RelayCommand(ShowEditPanel, CanEditItems);
            _viewModel.RemoveCommand = new RelayCommand(RemoveItems, CanRemoveItems);
        }

        private void ShowAddPanel(object parameter)
        {
            _viewModel.AddEntryViewModel.IsOpen = true;
        }

        private bool CanEditItems(object parameter)
        {
            return !_viewModel.EditEntryViewModel.IsOpen
                && _viewModel.SelectedResult != null;
        }

        private void ShowEditPanel(object parameter)
        {
            _viewModel.EditEntryViewModel.Item = _viewModel.SelectedResult;
            _viewModel.EditEntryViewModel.IsOpen = true;
        }

        private void Search(object parameter)
        {
            IEnumerable<PasswordEntry> searchResults = _passwordSheetFactory.Get().GetAll();
            if (!String.IsNullOrEmpty(_viewModel.SearchText))
            {
                searchResults = searchResults.Where(entry => entry.Title.Contains(_viewModel.SearchText));
            }

            _viewModel.SearchResults = new ObservableCollection<PasswordEntry>(searchResults);
        }

        private void RemoveItems(object parameter)
        {
            var entry = _viewModel.SelectedResult;
            _viewModel.SearchResults.Remove(entry);
            var sheet = _passwordSheetFactory.Get();
            sheet.Delete(entry.Id);
        }

        private bool CanRemoveItems(object parameter)
        {
            return _viewModel.SelectedResult != null;
        }
    }
}
