using System.Windows;
using System.Windows.Input;
namespace PasswordBook.Views.MainWindow
{
    public partial class MainWindowView
    {
        private MainWindowViewModel _viewModel;

        public MainWindowView(MainWindowViewModel viewModel)
        {
            _viewModel = viewModel;
            DataContext = _viewModel;
            InitializeComponent();
        }

        private void TextBoxGotFocus(object sender, RoutedEventArgs e)
        {
            searchButton.IsDefault = true;
        }

        private void TextBoxLostFocus(object sender, RoutedEventArgs e)
        {
            searchButton.IsDefault = false;
        }

        public void ItemDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (_viewModel.CommandBarViewModel.EditCommand.CanExecute(null))
            {
                _viewModel.CommandBarViewModel.EditCommand.Execute(null);
            }
        }
    }
}
