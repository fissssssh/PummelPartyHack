using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace PummelPartyHack.WPF
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Dispatcher.UnhandledException += (sender, unhandledExceptionEventArgs) =>
            {
                MessageBox.Show($"{unhandledExceptionEventArgs.Exception.Message}\n\nIf this problem persists, please commit issue to github repo", "错误", MessageBoxButton.OK, MessageBoxImage.Error);
                unhandledExceptionEventArgs.Handled = true;
            };
            base.OnStartup(e);
        }
    }
}
