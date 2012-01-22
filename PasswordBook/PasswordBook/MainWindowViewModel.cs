using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using PasswordBook.Model;
using System.ComponentModel;
using PasswordBook.Contracts;

namespace PasswordBook
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        public MainWindowViewModel(
            IEnumerable<IViewModelBehavior<MainWindowViewModel>> behaviors,
            AddEntryViewModel addEntryViewModel,
            AddEntryViewModel editEntryViewModel,
            MasterPasswordEntryViewModel masterPassword)
        {
            AddEntryViewModel = addEntryViewModel;
            EditEntryViewModel = editEntryViewModel;
            MasterPasswordEntryViewModel = masterPassword;

            behaviors.InitializeAll(this);
        }

        private ObservableCollection<PasswordEntry> _searchResults;
        private PasswordEntry _selectedResult;

        public ICommand SearchCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand RemoveCommand { get; set; }

        public AddEntryViewModel EditEntryViewModel { get; set; }

        public AddEntryViewModel AddEntryViewModel { get; set; }

        public MasterPasswordEntryViewModel MasterPasswordEntryViewModel { get; set; }

        public ObservableCollection<PasswordEntry> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged("SearchResults");
            }
        }

        public PasswordEntry SelectedResult
        {
            get { return _selectedResult; }
            set
            {
                _selectedResult = value;
                OnPropertyChanged("SelectedResult");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
