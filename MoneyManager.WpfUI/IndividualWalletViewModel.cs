using System.Collections.ObjectModel;
using System.Windows.Input;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;
using Microsoft.Extensions.DependencyInjection;

namespace MoneyManager.WpfUI.ViewModels
{
    public class IndividualWalletViewModel : ViewModelBase
    {
        private readonly IAppService service;
        private readonly INavigationService navigationService;
        public int WalletId { get; set; }
        private WalletDetailsDto details;

        public WalletDetailsDto Details
        {
            get { return details; }
            set { details = value; OnPropertyChanged("Details"); }
        }

        public ICommand BackCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand RemoveTransactionCommand { get; }
        public ICommand ShowTransactionDetailsCommand { get; }

        public IndividualWalletViewModel(IAppService service, INavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;

            BackCommand = new RelayCommand(param => this.navigationService.GoBack());
            AddTransactionCommand = new RelayCommand(AddTransaction);
            RemoveTransactionCommand = new RelayCommand(RemoveTransaction);
            ShowTransactionDetailsCommand = new RelayCommand(ShowTransactionDetails);
        }

        public void LoadDetails()
        {
            Details = service.GetWalletDetails(WalletId);
        }

        private void AddTransaction(object? parameter)
        {
            var addPage = App.ServiceProvider.GetRequiredService<AddTransactionPage>();
            if (addPage.DataContext is AddTransactionPageViewModel vm)
            {
                vm.WalletId = this.WalletId;
            }
            navigationService.NavigateTo(addPage);
        }

        private void RemoveTransaction(object? parameter)
        {
            if (parameter is int id)
            {
                service.RemoveTransaction(id);
                LoadDetails();
            }
        }

        private void ShowTransactionDetails(object? parameter)
        {
            if (parameter is TransactionListDto transaction)
            {
                TransactionDetailsDto? transactionDetails = service.GetTransactionDetails(transaction.Id);
                if (transactionDetails != null)
                {
                    navigationService.NavigateTo(new IndividualTransaction(transactionDetails));
                }
            }
        }
    }
}