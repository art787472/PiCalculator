using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using IOCDependencyInjection;
using PiCalculator.Contract;
using PiCalculator.Presenter;

namespace PiCalculator
{
    /// <summary>
    /// App.xaml 的互動邏輯
    /// </summary>
    public partial class App : Application
    {
        private void Application_Startup(object sender, StartupEventArgs e)
        {
            var collection = new ServiceCollection();
            collection.AddSingleton<IPiCalcPresenter, PiCalcPresenter>();
            collection.AddSingleton<IPiCalcView, MainWindow>();
            collection.AddSingleton<Window, MainWindow>();
            var provider = collection.BuildProvider();
            var window = provider.GetService<Window>();
            window.Show();

        }
    }
}
