using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Windows.Input;
using System.Security;
using System.Windows;

namespace PasswordBook.Views.MasterPasswordEntry
{
    public class MasterPasswordEntryViewModel : INotifyPropertyChanged
    {
        private Func<string> _passwordFunc;
        private bool _isOpen = true;
        private ICommand _submitCommand;
        private string _errorMessage;

        public MasterPasswordEntryViewModel(IEnumerable<IViewModelBehavior<MasterPasswordEntryViewModel>> behaviors)
        {
            behaviors.InitializeAll(this);
        }

        public Func<string> PasswordFunc
        {
            get { return _passwordFunc; }
            set
            {
                _passwordFunc = value;
            }
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

        public ICommand SubmitCommand
        {
            get { return _submitCommand; }
            set
            {
                _submitCommand = value;
                OnPropertyChanged("SubmitCommand");
            }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged("ErrorMessage");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
