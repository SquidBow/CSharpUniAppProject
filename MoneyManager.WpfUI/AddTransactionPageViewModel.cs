using System;
using System.Collections.Generic;
using System.Windows.Input;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;

namespace MoneyManager.WpfUI.ViewModels
{
    public class AddTransactionPageViewModel : ViewModelBase
    {
        private readonly IAppService service;
        private readonly INavigationService navigationService;
        public int WalletId { get; set; }

        private string sum;
        private string description;
        private string selectedType;

        public string Sum
        {
            get { return sum; }
            set { sum = value; OnPropertyChanged("Sum"); }
        }

        public string Description
        {
            get { return description; }
            set { description = value; OnPropertyChanged("Description"); }
        }

        public List<string> TransactionTypes { get; } = new List<string> { "Cafe", "Groceries", "Car", "None" };

        public string SelectedType
        {
            get { return selectedType; }
            set { selectedType = value; OnPropertyChanged("SelectedType"); }
        }

        public ICommand SaveCommand { get; }
        public ICommand CancelCommand { get; }

        public AddTransactionPageViewModel(IAppService service, INavigationService navigationService)
        {
            this.service = service;
            this.navigationService = navigationService;
            this.selectedType = "None";

            SaveCommand = new RelayCommand(Save);
            CancelCommand = new RelayCommand(param => this.navigationService.GoBack());
        }

        private void Save(object? parameter)
        {
            if (decimal.TryParse(Sum, out decimal amount))
            {
                service.AddTransaction(WalletId, amount, SelectedType, Description ?? "");
                navigationService.GoBack();
            }
        }
    }
}