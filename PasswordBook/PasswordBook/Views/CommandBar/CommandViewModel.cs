using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PasswordBook.Views.CommandBar
{
    public class CommandViewModel : INotifyPropertyChanged
    {
        public CommandViewModel(ICommand baseCommand)
        {
            Command = baseCommand;
        }

        public ICommand Command { get; private set; }

        public string ToolTip { get; set; }

        public string ImageSource { get; set; }

        public string Text { get; set; }

        public bool IsDefault { get; set; }

        public bool IsCancel { get; set; }

        private void RaisePropertyChanged(string propertyName)
        {
            var propertyChanged = PropertyChanged;
            if (propertyChanged != null)
            {
                propertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}
