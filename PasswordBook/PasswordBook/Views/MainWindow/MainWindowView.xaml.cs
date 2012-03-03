using System.Windows;
namespace PasswordBook.Views.MainWindow
{
    public partial class MainWindowView
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;
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
    }
}
