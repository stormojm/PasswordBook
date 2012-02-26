namespace PasswordBook.Views.MainWindow
{
    public partial class MainWindowView
    {
        public MainWindowView(MainWindowViewModel viewModel)
        {
            DataContext = viewModel;
            InitializeComponent();
        }
    }
}
