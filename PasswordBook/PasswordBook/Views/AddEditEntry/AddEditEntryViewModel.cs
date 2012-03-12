using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using PasswordBook.Contracts;

namespace PasswordBook.Views.AddEditEntry
{
    public class AddEditEntryViewModel : INotifyPropertyChanged
    {
        private bool _isOpen;
        private string _userName;
        private string _password;
        private string _title;
        private PasswordEntry _item;

        public AddEditEntryViewModel(IEnumerable<IViewModelBehavior<AddEditEntryViewModel>> behaviors)
        {
            behaviors.InitializeAll(this);
        }

        public bool IsOpen
        {
            get { return _isOpen; }
            set
            {
                _isOpen = value;
                OnPropertyChanged("IsOpen");
            }
        }

        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                OnPropertyChanged("Title");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                OnPropertyChanged("UserName");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged("Password");
                CommandManager.InvalidateRequerySuggested();
            }
        }

        public PasswordEntry Item
        {
            get { return _item; }
            set
            {
                _item = value;
                if (_item != null)
                {
                    Title = _item.Title;
                    Password = _item.Password;
                    UserName = _item.UserName;
                }
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
