using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using MoneyManager.Models;

namespace MoneyManager.WpfUI
{
    /// <summary>
    /// Interaction logic for IndividualWallet.xaml
    /// </summary>
    public partial class IndividualWallet : Page
    {
        ObservableCollection<Transaction> transactions;
        Wallet currentWallet;
        IAppService service;

        public IndividualWallet(Wallet wallet, IAppService serviceIn)
        {
            InitializeComponent();
            transactions = new ObservableCollection<Transaction>(serviceIn.GetWalletTransactions(wallet.Id));
            this.DataContext = wallet;
            TrasactionList.ItemsSource = transactions;
            currentWallet = wallet;
            service = serviceIn;
        }

        private void ShowIndividualTransactio(object sender, MouseButtonEventArgs e)
        {
            Transaction selectedTransaction = (Transaction)TrasactionList.SelectedItem;
            NavigationService.Navigate(new IndividualTransaction(selectedTransaction));
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            AddTransactionPage addPage = new(transactions, currentWallet.Id, service);
            NavigationService.Navigate(addPage);
        }

        private void SubButton(object sender, RoutedEventArgs e)
        {
            Transaction selectedTransaction = (Transaction)TrasactionList.SelectedItem;
            transactions.Remove(selectedTransaction);
        }
    }
}
