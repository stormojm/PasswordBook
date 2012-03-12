using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PasswordBook.Views.CommandBar
{
    public class DefaultCommandViewModel : CommandViewModel
    {
        public DefaultCommandViewModel(ICommand baseCommand) : base(baseCommand)
        {
            IsDefault = true;
        }
    }
}
