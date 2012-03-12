using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Windows.Input;

namespace PasswordBook.Views.CommandBar
{
    public class CommandCollectionViewModelBehavior : IViewModelBehavior<CommandBarViewModel>
    {
        private CommandBarViewModel _viewModel;

        public void Initialize(CommandBarViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.Commands = new ObservableCollection<CommandViewModel>();

            _viewModel.Commands.Add(ConvertCommand(_viewModel.AddCommand, "Add a new password entry", "pack://application:,,,/Resources/Add.ico", null));
            _viewModel.Commands.Add(ConvertCommand(_viewModel.EditCommand, "Edit selected password entry", "pack://application:,,,/Resources/Edit.ico", null));
            _viewModel.Commands.Add(ConvertCommand(_viewModel.RemoveCommand, "Remove selected password entries", "pack://application:,,,/Resources/Delete.ico", null));

            _viewModel.Commands.Add(ConvertDefaultCommand(_viewModel.AddSaveCommand, "Save", null, "Save"));

            var cancelCommandViewModel = ConvertCommand(_viewModel.AddCancelCommand, "Cancel", null, "Cancel");
            cancelCommandViewModel.IsCancel = true;
            _viewModel.Commands.Add(cancelCommandViewModel);

            _viewModel.Commands.Add(ConvertDefaultCommand(_viewModel.EditSaveCommand, "Save", null, "Save"));

            var editCancelCommandViewModel = ConvertCommand(_viewModel.EditCancelCommand, "Cancel", null, "Cancel");
            editCancelCommandViewModel.IsCancel = true;
            _viewModel.Commands.Add(editCancelCommandViewModel);
        }

        private CommandViewModel ConvertCommand(ICommand baseCommand, string toolTip, string imageUrl, string text)
        {
            return new CommandViewModel(baseCommand) { ImageSource = imageUrl, ToolTip = toolTip, Text = text };
        }

        private CommandViewModel ConvertDefaultCommand(ICommand baseCommand, string toolTip, string imageUrl, string text)
        {
            return new DefaultCommandViewModel(baseCommand) { ImageSource = imageUrl, ToolTip = toolTip, Text = text };
        }
    }
}
