using PasswordBook.Model;
using PasswordBook.Model.Encryption;
namespace PasswordBook
{
    public partial class MainWindow
    {
        public MainWindow(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
