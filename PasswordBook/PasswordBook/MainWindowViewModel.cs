using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;
using System.Collections.ObjectModel;
using PasswordBook.Model;
using System.ComponentModel;

namespace PasswordBook
{
    public class MainWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<PasswordEntry> _searchResults;

        public ICommand SearchCommand { get; set; }

        public ICommand AddCommand { get; set; }

        public AddEntryViewModel AddEntryViewModel { get; set; }

        public ObservableCollection<PasswordEntry> SearchResults
        {
            get { return _searchResults; }
            set
            {
                _searchResults = value;
                OnPropertyChanged("SearchResults");
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
