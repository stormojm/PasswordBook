using System.Windows;

namespace PasswordBook.Views.MasterPasswordEntry
{
    public partial class MasterPasswordEntryView
    {
        public MasterPasswordEntryView()
        {
            DataContextChanged += new DependencyPropertyChangedEventHandler(OnDataContextChanged);
            InitializeComponent();
        }

        private void OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var viewModel = e.NewValue as MasterPasswordEntryViewModel;
            viewModel.PasswordFunc = () => password.Password;
        }
    }
}
