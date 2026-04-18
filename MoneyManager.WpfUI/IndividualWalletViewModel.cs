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

        public ObservableCollection<TransactionListDto> ShownTransactions { get; set; } = new();
        private string currentSort = "Id";
        private bool sortAscending = true;
        public string FilterText = "";

        public ICommand BackCommand { get; }
        public ICommand AddTransactionCommand { get; }
        public ICommand RemoveTransactionCommand { get; }
        public ICommand EditTransactionCommand { get; }
        public ICommand ShowTransactionDetailsCommand { get; }

        public IndividualWalletViewModel(IAppService service, INavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;

            BackCommand = new RelayCommand(param => this.navigationService.GoBack());
            AddTransactionCommand = new RelayCommand(AddTransaction);
            RemoveTransactionCommand = new RelayCommand(RemoveTransaction);
            EditTransactionCommand = new RelayCommand(EditTransaction);
            ShowTransactionDetailsCommand = new RelayCommand(ShowTransactionDetails);
        }

        public void LoadDetails()
        {
            Details = service.GetWalletDetails(WalletId);
            SortBy("");
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

        private async void RemoveTransaction(object? parameter)
        {
            if (parameter is int id)
            {
                service.RemoveTransaction(id);
                await service.SaveData();
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

        private void EditTransaction(object? parameter)
        {
            if (parameter is int id)
            {
                var transaction = Details.Transactions.FirstOrDefault(t => t.Id == id);
                if (transaction == null) return;
                var addPage = App.ServiceProvider.GetRequiredService<AddTransactionPage>();
                if (addPage.DataContext is AddTransactionPageViewModel vm)
                {
                    vm.Init(transaction);
                }
                navigationService.NavigateTo(addPage);
            }
        }

        public void SortBy(string field)
        {
            if (field != "")
            {
                if (currentSort == field)
                    sortAscending = !sortAscending;
                else
                {
                    currentSort = field;
                    sortAscending = true;
                }
            }

            UpdateShownTransactions(FilterText);
        }

        public void UpdateShownTransactions(string filterText)
        {
            if (Details == null) return;
            FilterText = filterText;
            var filtered = Details.Transactions.Where(t => t.Description.Contains(filterText, StringComparison.OrdinalIgnoreCase));
            var ordered = currentSort switch
            {
                "Sum" => sortAscending ? filtered.OrderBy(t => t.Sum) : filtered.OrderByDescending(t => t.Sum),
                "Type" => sortAscending ? filtered.OrderBy(t => t.Type) : filtered.OrderByDescending(t => t.Type),
                "Description" => sortAscending ? filtered.OrderBy(t => t.Description) : filtered.OrderByDescending(t => t.Description),
                "Date" => sortAscending ? filtered.OrderBy(t => t.Date) : filtered.OrderByDescending(t => t.Date),
                _ => sortAscending ? filtered.OrderBy(t => t.Id) : filtered.OrderByDescending(t => t.Id)
            };
            ShownTransactions = new ObservableCollection<TransactionListDto>(ordered);
            OnPropertyChanged("ShownTransactions");
        }
    }
}