using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using PasswordBook.Model;
using PasswordBook.Contracts;
using PasswordBook.Contracts.Views;
using PasswordBook.Core;

namespace PasswordBook
{
    public class PasswordBookModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new PasswordSheetFactory())
                .As<IPasswordSheetFactory>()
                .SingleInstance();

            builder.Register(c => new MainWindowViewModel(
                                    c.Resolve<IEnumerable<IViewModelBehavior<MainWindowViewModel>>>(),
                                    c.ResolveNamed<AddEntryViewModel>("addEntry"),
                                    c.ResolveNamed<AddEntryViewModel>("editEntry"),
                                    c.Resolve<MasterPasswordEntryViewModel>()))
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new AddEntryViewModel(new [] { c.Resolve<AddEntryViewModelLogic>() } ))
                .Named<AddEntryViewModel>("addEntry")
                .InstancePerDependency();

            builder.Register(c => new AddEntryViewModel(new[] { c.Resolve<EditEntryViewModelBehavior>() }))
                .Named<AddEntryViewModel>("editEntry")
                .InstancePerDependency();

            builder.Register(c => new EditEntryViewModelBehavior(c.Resolve<IPasswordSheetFactory>()))
                .As<IViewModelBehavior<AddEntryViewModel>>()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new AddEntryViewModelLogic(c.Resolve<IPasswordSheetFactory>()))
                .As<IViewModelBehavior<AddEntryViewModel>>()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new MainWindowFactory(c.Resolve<Func<MainWindowViewModel>>()))
                .As<IMainWindowView>()
                .InstancePerDependency();

            builder.Register(c => new MainWindowViewModelLogic(c.Resolve<IPasswordSheetFactory>()))
                .As<IViewModelBehavior<MainWindowViewModel>>()
                .InstancePerDependency();

            builder.Register(c => new SearchResultsViewModelLogic(c.Resolve<IEventAggregator>(), c.Resolve<IPasswordSheetFactory>()))
                .As<IViewModelBehavior<MainWindowViewModel>>()
                .InstancePerDependency();

            builder.Register(c => new MasterPasswordEntryViewModelBehavior(c.Resolve<IPasswordSheetFactory>(), c.Resolve<IEventAggregator>()))
                .As<IViewModelBehavior<MasterPasswordEntryViewModel>>()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new MasterPasswordEntryViewModel(c.Resolve<IEnumerable<IViewModelBehavior<MasterPasswordEntryViewModel>>>()))
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new EventAggregator())
                .As<IEventAggregator>()
                .SingleInstance();
        }
    }
}
