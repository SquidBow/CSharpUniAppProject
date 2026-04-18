using MoneyManager.Models;
using MoneyManager.WpfUI.Services;
using MoneyManager.WpfUI.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace MoneyManager.WpfUI
{
    public partial class IndividualWallet : Page
    {
        public IndividualWallet(IndividualWalletViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            this.Loaded += (s, e) => viewModel.LoadDetails();
        }

        private void UpdateShownTransactions(object sender, TextChangedEventArgs e)
        {
            if (DataContext is IndividualWalletViewModel vm)
            {
                vm.UpdateShownTransactions(((TextBox)sender).Text);
            }
        }

        private void SortById(object sender, RoutedEventArgs e) {
            if (DataContext is IndividualWalletViewModel vm)
                vm.SortBy("Id");
        }
        private void SortBySum(object sender, RoutedEventArgs e)
        {
            if (DataContext is IndividualWalletViewModel vm)
                vm.SortBy("Sum");
        }
        private void SortByType(object sender, RoutedEventArgs e)
        {
            if (DataContext is IndividualWalletViewModel vm)
                vm.SortBy("Type");
        }
        private void SortByDescription(object sender, RoutedEventArgs e)
        {
            if (DataContext is IndividualWalletViewModel vm)
                vm.SortBy("Description");
        }
        private void SortByDate(object sender, RoutedEventArgs e)
        {
            if (DataContext is IndividualWalletViewModel vm)
                vm.SortBy("Date");
        }
    }
}