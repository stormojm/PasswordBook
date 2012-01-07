using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using PasswordBook.Model;

namespace PasswordBook
{
    public class MainWindowViewModelLogic
    {
        private MainWindowViewModel _viewModel;

        public MainWindowViewModelLogic(MainWindowViewModel viewModel, AddEntryViewModel addEntryViewModel)
        {
            _viewModel = viewModel;
            _viewModel.SearchCommand = new RelayCommand(Search);
            _viewModel.AddCommand = new RelayCommand(ShowAddPanel, obj => !addEntryViewModel.IsOpen);
            _viewModel.AddEntryViewModel = addEntryViewModel;
        }

        private void ShowAddPanel(object parameter)
        {
            _viewModel.AddEntryViewModel.IsOpen = true;
        }

        private void Search(object parameter)
        {
            _viewModel.SearchResults = new ObservableCollection<PasswordEntry>
            {
                new PasswordEntry { Password = "1234", Title = "Thing", UserName = "Jarrod.Stormo" },
            };
        }
    }
}
