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
    /// Interaction logic for AddTransactionPage.xaml
    /// </summary>
    public partial class AddTransactionPage : Page
    {
        ObservableCollection<Transaction> transactions;
        int walletID;
        IAppService service;

        public AddTransactionPage(ObservableCollection<Transaction> transactionsIn, int walletIDIn, IAppService serviceIn)
        {
            InitializeComponent();
            TypeSelector.ItemsSource = System.Enum.GetValues(typeof(Spending));
            TypeSelector.SelectedIndex = 0;
            transactions = transactionsIn;
            walletID = walletIDIn;
            service = serviceIn;
        }
        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(SumInput.Text, out decimal sum) && TypeSelector.SelectedItem != null)
            {
                transactions.Add(new Transaction(service.FindMaxTransactionID(), walletID, sum, (Spending)TypeSelector.SelectedItem, DescInput.Text));
                NavigationService.GoBack();
            }
        }
    }
}
