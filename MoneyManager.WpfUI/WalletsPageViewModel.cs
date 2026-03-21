using System.Collections.ObjectModel;
using System.Windows.Input;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyManager.WpfUI.ViewModels
{
    public class WalletsPageViewModel : ViewModelBase
    {
        private readonly IAppService service;
        private readonly INavigationService navigationService;
        private ObservableCollection<WalletListDto> wallets;

        public ObservableCollection<WalletListDto> Wallets
        {
            get { return wallets; }
            set
            {
                wallets = value;
                OnPropertyChanged("Wallets");
            }
        }

        public ICommand RemoveCommand { get; }
        public ICommand AddCommand { get; }

        public ICommand ShowDetailsCommand { get; }

        private void ShowDetails(object? parameter)
        {
            if (parameter is WalletListDto selectedWallet)
            {
                var detailsPage = App.ServiceProvider.GetRequiredService<IndividualWallet>();

                if (detailsPage.DataContext is IndividualWalletViewModel vm)
                {
                    vm.WalletId = selectedWallet.Id;
                    vm.LoadDetails();
                }

                navigationService.NavigateTo(detailsPage);
            }
        }

        public WalletsPageViewModel(IAppService service, INavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            LoadWallets();
            RemoveCommand = new RelayCommand(RemoveWallet);
            AddCommand = new RelayCommand(AddWallet);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
        }

        public void LoadWallets()
        {
            var data = service.GetWalletsForList();
            Wallets = new ObservableCollection<WalletListDto>(data);
        }

        private void RemoveWallet(object? parameter)
        {
            if (parameter is int id)
            {
                service.RemoveWallet(id);
                LoadWallets();
            }
        }

        private void AddWallet(object? parameter)
        {
            var addPage = App.ServiceProvider.GetRequiredService<AddWalletPage>();
            navigationService.NavigateTo(addPage);
        }
    }
}