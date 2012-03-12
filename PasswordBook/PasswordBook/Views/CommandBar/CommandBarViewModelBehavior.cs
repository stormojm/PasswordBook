using System.Windows.Input;
using PasswordBook.Contracts;
using PasswordBook.Views.CommandBar;
using PasswordBook.Views.MainWindow;

namespace PasswordBook.Views.CommandBar
{
    public class CommandBarViewModelBehavior : IViewModelBehavior<CommandBarViewModel>, IViewModelBehavior<MainWindowViewModel>
    {
        private CommandBarViewModel _viewModel;
        private IPasswordSheetFactory _passwordSheetFactory;
        private MainWindowViewModel _mainViewModel;

        public CommandBarViewModelBehavior(IPasswordSheetFactory passwordSheetFactory)
        {
            _passwordSheetFactory = passwordSheetFactory;
        }

        public void Initialize(CommandBarViewModel viewModel)
        {
            _viewModel = viewModel;

            _viewModel.AddCommand = new RelayCommand(ShowAddPanel, CanAddItems);
            _viewModel.EditCommand = new RelayCommand(ShowEditPanel, CanEditItems);
            _viewModel.RemoveCommand = new RelayCommand(RemoveItems, CanRemoveItems);
        }

        public void Initialize(MainWindowViewModel viewModel)
        {
            _mainViewModel = viewModel;
        }

        private void ShowAddPanel(object parameter)
        {
            _mainViewModel.AddEntryViewModel.IsOpen = true;
        }

        private bool CanEditItems(object parameter)
        {
            return !_mainViewModel.AddEntryViewModel.IsOpen
                && !_mainViewModel.EditEntryViewModel.IsOpen
                && _mainViewModel.SelectedResult != null;
        }

        private void ShowEditPanel(object parameter)
        {
            _mainViewModel.EditEntryViewModel.Item = _mainViewModel.SelectedResult;
            _mainViewModel.EditEntryViewModel.IsOpen = true;
        }

        private void RemoveItems(object parameter)
        {
            var entry = _mainViewModel.SelectedResult;
            _mainViewModel.SearchResults.Remove(entry);
            var sheet = _passwordSheetFactory.Get();
            sheet.Delete(entry.Id);
        }

        private bool CanRemoveItems(object parameter)
        {
            return !_mainViewModel.AddEntryViewModel.IsOpen
                && !_mainViewModel.EditEntryViewModel.IsOpen
                && _mainViewModel.SelectedResult != null;
        }

        private bool CanAddItems(object parameter)
        {
            return !_mainViewModel.AddEntryViewModel.IsOpen
                && !_mainViewModel.EditEntryViewModel.IsOpen;
        }
    }
}
