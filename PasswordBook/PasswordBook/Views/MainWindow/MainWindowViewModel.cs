using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;
using PasswordBook.Contracts;
using PasswordBook.Views.AddEditEntry;
using PasswordBook.Views.CommandBar;
using PasswordBook.Views.MasterPasswordEntry;

namespace PasswordBook.Views.MainWindow
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private string _searchText;
        private ObservableCollection<PasswordEntry> _searchResults;
        private PasswordEntry _selectedResult;

        public MainWindowViewModel(
            IEnumerable<IViewModelBehavior<MainWindowViewModel>> behaviors,
            AddEditEntryViewModel addEntryViewModel,
            AddEditEntryViewModel editEntryViewModel,
            MasterPasswordEntryViewModel masterPassword,
            CommandBarViewModel commandBarViewModel)
        {
            AddEntryViewModel = addEntryViewModel;
            EditEntryViewModel = editEntryViewModel;
            MasterPasswordEntryViewModel = masterPassword;
            CommandBarViewModel = commandBarViewModel;

            behaviors.InitializeAll(this);
        }

        public ICommand SearchCommand { get; set; }

        public string SearchText
        {
            get { return _searchText; }
            set
            {
                _searchText = value;
                OnPropertyChanged("SearchText");
            }
        }

        public AddEditEntryViewModel EditEntryViewModel { get; set; }

        public AddEditEntryViewModel AddEntryViewModel { get; set; }

        public MasterPasswordEntryViewModel MasterPasswordEntryViewModel { get; set; }

        public CommandBarViewModel CommandBarViewModel { get; set; }

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
