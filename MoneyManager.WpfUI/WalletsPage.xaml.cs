using MoneyManager.Models;
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

namespace MoneyManager.WpfUI
{
    /// <summary>
    /// Interaction logic for WalletsPage.xaml
    /// </summary>
    public partial class WalletsPage : Page
    {
        ObservableCollection<Wallet> wallets;
        IAppService service;
        public WalletsPage(IDataStorage storage, IAppService serviceInput)
        {
            InitializeComponent();
            wallets = new ObservableCollection<Wallet>(storage.Wallets);
            WalletsList.ItemsSource = wallets;
            service = serviceInput;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {

        }

        private void ShowIndividualWallet(object sender, MouseButtonEventArgs e)
        {
            Wallet selectedWallet = (Wallet)WalletsList.SelectedItem;
            NavigationService.Navigate(new IndividualWallet(selectedWallet, service));
        }
    }
}
