using MoneyManager.Services;
using MoneyManager.Storage;
using System.Configuration;
using System.Data;
using System.Windows;

namespace MoneyManager.WpfUI
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            DataStorage storage = new DataStorage();
            AppService service = new AppService(storage);
            MainWindow window = new MainWindow(storage, service);
            window.Show();
        }
    }

}
