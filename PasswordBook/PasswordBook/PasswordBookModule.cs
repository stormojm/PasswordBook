﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autofac;
using PasswordBook.Model;
using PasswordBook.Contracts;
using PasswordBook.Contracts.Views;
using PasswordBook.Core;
using PasswordBook.Views.AddEditEntry;
using PasswordBook.Views.MainWindow;
using PasswordBook.Views.MasterPasswordEntry;

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
                                    c.ResolveNamed<AddEditEntryViewModel>("addEntry"),
                                    c.ResolveNamed<AddEditEntryViewModel>("editEntry"),
                                    c.Resolve<MasterPasswordEntryViewModel>()))
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new AddEditEntryViewModel(new[] { c.Resolve<AddEntryViewModelBehavior>() }))
                .Named<AddEditEntryViewModel>("addEntry")
                .InstancePerDependency();

            builder.Register(c => new AddEditEntryViewModel(new[] { c.Resolve<EditEntryViewModelBehavior>() }))
                .Named<AddEditEntryViewModel>("editEntry")
                .InstancePerDependency();

            builder.Register(c => new EditEntryViewModelBehavior(c.Resolve<IPasswordSheetFactory>(), c.Resolve<IEventAggregator>()))
                .As<IViewModelBehavior<AddEditEntryViewModel>>()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new AddEntryViewModelBehavior(c.Resolve<IPasswordSheetFactory>(), c.Resolve<IEventAggregator>()))
                .As<IViewModelBehavior<AddEditEntryViewModel>>()
                .AsSelf()
                .InstancePerDependency();

            builder.Register(c => new MainWindowFactory(c.Resolve<Func<MainWindowViewModel>>()))
                .As<IMainWindowView>()
                .InstancePerDependency();

            builder.Register(c => new MainSideBarViewModelBehavior(c.Resolve<IPasswordSheetFactory>()))
                .As<IViewModelBehavior<MainWindowViewModel>>()
                .InstancePerDependency();

            builder.Register(c => new SearchResultsViewModelBehavior(c.Resolve<IEventAggregator>(), c.Resolve<IPasswordSheetFactory>()))
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
