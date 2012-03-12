using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PasswordBook.Views.CommandBar
{
    public class CommandBarViewModel
    {
        public CommandBarViewModel(IEnumerable<IViewModelBehavior<CommandBarViewModel>> behaviors)
        {
            behaviors.InitializeAll(this);
        }

        public ObservableCollection<CommandViewModel> Commands { get; set; }

        public ICommand AddCommand { get; set; }

        public ICommand EditCommand { get; set; }

        public ICommand RemoveCommand { get; set; }

        public ICommand EditSaveCommand { get; set; }

        public ICommand EditCancelCommand { get; set; }

        public ICommand AddSaveCommand { get; set; }

        public ICommand AddCancelCommand { get; set; }
    }
}
