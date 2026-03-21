using System;
using System.Collections.Generic;
using System.Windows.Input;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;

namespace MoneyManager.WpfUI.ViewModels
{
    public class AddWalletPageViewModel : ViewModelBase
    {
        private readonly IAppService service;
        private readonly INavigationService navigationService;

        private string walletName;
        private string selectedCurrency;

        public string WalletName
        {
            get { return walletName; }
            set { walletName = value; OnPropertyChanged("WalletName"); }
        }

        public List<string> CurrenciesList { get; } = new List<string> { "UAH", "USD", "EUR" };

        public string SelectedCurrency
        {
            get { return selectedCurrency; }
            set { selectedCurrency = value; OnPropertyChanged("SelectedCurrency"); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddWalletPageViewModel(IAppService service, INavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.selectedCurrency = "UAH";

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(param => this.navigationService.GoBack());
        }

        private void Save(object? parameter)
        {
            if (!string.IsNullOrWhiteSpace(WalletName))
            {
                service.AddWallet(WalletName, SelectedCurrency);
                navigationService.GoBack();
            }
        }
    }
}