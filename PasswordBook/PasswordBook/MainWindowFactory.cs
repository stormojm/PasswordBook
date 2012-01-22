using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Model;
using PasswordBook.Contracts.Views;

namespace PasswordBook
{
    public class MainWindowFactory : IMainWindowView
    {
        private Func<MainWindowViewModel> _mainWindowViewModelFactory;

        public MainWindowFactory(Func<MainWindowViewModel> mainWindowViewModelFactory)
        {
            _mainWindowViewModelFactory = mainWindowViewModelFactory;
        }

        public void ShowDialog()
        {
            var viewModelInterface = _mainWindowViewModelFactory();
            var view = new MainWindow(viewModelInterface);

            view.ShowDialog();
        }
    }
}
