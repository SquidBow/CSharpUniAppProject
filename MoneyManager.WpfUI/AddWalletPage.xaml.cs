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
    /// Interaction logic for AddWalletPage.xaml
    /// </summary>
    public partial class AddWalletPage : Page
    {
        ObservableCollection<Wallet> wallets;
        IAppService service;

        public AddWalletPage(ObservableCollection<Wallet> walletsIn, IAppService serviceIn)
        {
            InitializeComponent();
            CurrencySelector.ItemsSource = Enum.GetValues(typeof(Currencies));
            wallets = walletsIn;
            service = serviceIn; 
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WalletName.Text) && CurrencySelector.SelectedItem != null)
            {
                wallets.Add(new Wallet(service.FindMaxWalletID(), WalletName.Text, (Currencies)CurrencySelector.SelectedItem));
                NavigationService.GoBack();
            }
        }
    }
}
