using Microsoft.Extensions.DependencyInjection;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using System.Windows.Input;

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

        public ObservableCollection<WalletListDto> ShownWallets { get; set; } = new();

        public string FilterText = "";

        public ICommand RemoveCommand { get; }
        public ICommand AddCommand { get; }
        public ICommand EditCommand { get; }

        public ICommand ShowDetailsCommand { get; }

        private string currentSort = "Id";
        private bool sortAscending = true;

        public void SortBy(string field)
        {
            if (field!= "")
            {
                if (currentSort == field)
                    sortAscending = !sortAscending;
                else
                {
                    currentSort = field;
                    sortAscending = true;
                }
            }
            
            UpdateShownWallets(FilterText);
        }

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
            EditCommand = new RelayCommand(EditWallet);
            ShowDetailsCommand = new RelayCommand(ShowDetails);
        }

        public void LoadWallets()
        {
            var data = service.GetWalletsForList();
            Wallets = new ObservableCollection<WalletListDto>(data);
            ShownWallets = new ObservableCollection<WalletListDto>(Wallets);
            SortBy("");
        }

        private async void RemoveWallet(object? parameter)
        {
            if (parameter is int id)
            {
                service.RemoveWallet(id);
                await service.SaveData();
                LoadWallets();
            }
        }

        private void AddWallet(object? parameter)
        {
            var addPage = App.ServiceProvider.GetRequiredService<AddWalletPage>();
            navigationService.NavigateTo(addPage);
        }

        private void EditWallet(object? parameter)
        {
            if (parameter is WalletListDto selectedWallet)
            {
                var wallet = service.GetWalletById(selectedWallet.Id);
                var addPage = App.ServiceProvider.GetRequiredService<AddWalletPage>();
                if (addPage.DataContext is AddWalletPageViewModel vm)
                {
                    vm.Init(wallet);
                }
                navigationService.NavigateTo(addPage);
            }
        }

        public void UpdateShownWallets(string filterText)
        {
            FilterText = filterText;
            var filtered = Wallets.Where(w => w.Name.Contains(filterText, StringComparison.OrdinalIgnoreCase));
            var ordered = currentSort switch
            {
                "Balance" => sortAscending ? filtered.OrderBy(w => w.Balance) : filtered.OrderByDescending(w => w.Balance),
                "Name" => sortAscending ? filtered.OrderBy(w => w.Name) : filtered.OrderByDescending(w => w.Name),
                "Id" => sortAscending ? filtered.OrderBy(w => w.Id) : filtered.OrderByDescending(w => w.Id)
            };
            ShownWallets = new ObservableCollection<WalletListDto>(ordered);
            OnPropertyChanged("ShownWallets");
        }
    }
}