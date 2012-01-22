using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PasswordBook.Model;
using PasswordBook.Contracts;

namespace PasswordBook
{
    public class MasterPasswordEntryViewModelBehavior : IViewModelBehavior<MasterPasswordEntryViewModel>
    {
        private MasterPasswordEntryViewModel _viewModel;
        private IPasswordSheetFactory _passwordSheetFactory;

        private static int _InstanceCount;

        public MasterPasswordEntryViewModelBehavior(IPasswordSheetFactory passwordSheetFactory)
        {
            _InstanceCount++;
            _passwordSheetFactory = passwordSheetFactory;
        }

        public void Initialize(MasterPasswordEntryViewModel viewModel)
        {
            _viewModel = viewModel;
            _viewModel.SubmitCommand = new RelayCommand(OnSubmit);
        }

        private void OnSubmit(object commandParameter)
        {
            bool created = _passwordSheetFactory.LoadSheet(_viewModel.PasswordFunc());

            if (created)
            {
                _viewModel.ErrorMessage = String.Empty;
                _viewModel.IsOpen = false;
            }
            else
            {
                _viewModel.ErrorMessage = "Unable to create";
            }
        }
    }
}
