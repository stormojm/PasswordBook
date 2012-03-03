using System;
using PasswordBook.Contracts;
using PasswordBook.Messages;

namespace PasswordBook.Views.MasterPasswordEntry
{
    public class MasterPasswordEntryViewModelBehavior : IViewModelBehavior<MasterPasswordEntryViewModel>
    {
        private MasterPasswordEntryViewModel _viewModel;
        private IPasswordSheetFactory _passwordSheetFactory;
        private IEventAggregator _eventBroker;

        private static int _InstanceCount;

        public MasterPasswordEntryViewModelBehavior(IPasswordSheetFactory passwordSheetFactory, IEventAggregator eventBroker)
        {
            _InstanceCount++;
            _passwordSheetFactory = passwordSheetFactory;
            _eventBroker = eventBroker;
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
                _eventBroker.Send(new MasterPasswordEntered());
            }
            else
            {
                _viewModel.ErrorMessage = "Invalid Password";
            }
        }
    }
}
