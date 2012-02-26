using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using PasswordBook.Contracts;
using PasswordBook.Messages;

namespace PasswordBook.Views.MainWindow
{
    public class SearchResultsViewModelBehavior : IViewModelBehavior<MainWindowViewModel>
    {
        private MainWindowViewModel _viewModel;
        private IEventAggregator _eventBroker;
        private IPasswordSheetFactory _passwordSheetFactory;

        public SearchResultsViewModelBehavior(IEventAggregator eventBroker, IPasswordSheetFactory passwordSheetFactory)
        {
            _eventBroker = eventBroker;
            _passwordSheetFactory = passwordSheetFactory;
        }

        public void Initialize(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.SearchCommand = new RelayCommand(Search);

            _eventBroker.Subscribe<MasterPasswordEntered>(OnMasterPasswordEntered);
        }

        private void OnMasterPasswordEntered(MasterPasswordEntered message)
        {
            Search(null);
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
    }
}
