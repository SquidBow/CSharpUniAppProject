using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Models;
using MoneyManager.Repositories;
using MoneyManager.Services;
using MoneyManager.Storage;
using System;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager.WpfUI
{
    public partial class App : Application
    {
        public static IServiceProvider ServiceProvider { get; private set; } = null!;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            services.AddSingleton<IDataStorage, MoneyManager.Storage.DataStorage>();
            services.AddTransient<IWalletRepository, MoneyManager.Repositories.WalletRepository>();
            services.AddTransient<ITransactionRepository, MoneyManager.Repositories.TransactionRepository>();
            services.AddTransient<IAppService, MoneyManager.Services.AppService>();

            services.AddTransient<ViewModels.WalletsPageViewModel>();
            services.AddTransient<WalletsPage>();
            services.AddTransient<MainWindow>();

            services.AddTransient<IndividualWallet>();
            services.AddTransient<ViewModels.IndividualWalletViewModel>();

            services.AddTransient<AddTransactionPage>();
            services.AddTransient<ViewModels.AddTransactionPageViewModel>();

            var frame = new Frame();
            var navService = new Services.NavigationService(frame);

            services.AddSingleton<Services.INavigationService>(navService);
            services.AddTransient<ViewModels.AddWalletPageViewModel>();
            services.AddTransient<AddWalletPage>();

            services.AddTransient<LoadingPage>();

            ServiceProvider = services.BuildServiceProvider();

            var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}
