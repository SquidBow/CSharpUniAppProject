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
using System.Windows.Shapes;

using MoneyManager.Models;

namespace MoneyManager.WpfUI
{
    /// <summary>
    /// Interaction logic for AddWalletWindow.xaml
    /// </summary>
    public partial class AddWalletWindow : Window
    {
        public string Name { get; private set; }
        public Currencies Currency { get; private set; }

        public AddWalletWindow()
        {
            InitializeComponent();

            CurrencySelector.ItemsSource = Enum.GetValues(typeof(Currencies));
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(WalletName.Text) && CurrencySelector.SelectedItem != null)
            {
                Name = WalletName.Text;
                Currency = (Currencies)CurrencySelector.SelectedItem;
                this.DialogResult = true;
            }
        }
    }
}
