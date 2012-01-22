using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using PasswordBook.Model;
using Autofac;
using PasswordBook.Contracts.Views;
using PasswordBook.Contracts;

namespace PasswordBook
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private void ApplicationStartup(object sender, StartupEventArgs e)
        {
            var builder = new ContainerBuilder();
            builder.RegisterModule(new PasswordBookModule());
            var container = builder.Build();
            var mainWindowView = container.Resolve<IMainWindowView>();
            mainWindowView.ShowDialog();

            var factory = container.Resolve<IPasswordSheetFactory>();
            factory.SaveSheet();
        }
    }
}
