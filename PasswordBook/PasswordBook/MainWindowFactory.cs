using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PasswordBook
{
    public class MainWindowFactory
    {
        public void ShowDialog()
        {
            var viewModelInterface = new MainWindowViewModel();
            var addEntryViewModelInterface = new AddEntryViewModel();
            var addEntryViewModelLogic = new AddEntryViewModelLogic(addEntryViewModelInterface);
            var viewModelLogic = new MainWindowViewModelLogic(viewModelInterface, addEntryViewModelInterface);
            var view = new MainWindow(viewModelInterface);

            view.ShowDialog();
        }
    }
}
