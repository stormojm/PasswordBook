using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PasswordBook
{
    /// <summary>
    /// Interaction logic for MasterPasswordEntry.xaml
    /// </summary>
    public partial class MasterPasswordEntry : UserControl
    {
        public MasterPasswordEntry()
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
