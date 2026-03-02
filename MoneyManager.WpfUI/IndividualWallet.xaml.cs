using System;
using System.Collections.Generic;
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
        public IndividualWallet(Wallet wallet, IAppService service)
        {
            InitializeComponent();
            List<Transaction> transactions = service.GetWalletTransactions(wallet.Id);
            this.DataContext = wallet;
            TrasactionList.ItemsSource = transactions;
        }

        private void ShowIndividualTransactio(object sender, MouseButtonEventArgs e)
        {
            Transaction selectedWallet = (Transaction)TrasactionList.SelectedItem;
            NavigationService.Navigate(new IndividualTransaction(selectedWallet));
        }
    }
}
