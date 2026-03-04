using MoneyManager.Models;
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

namespace MoneyManager.WpfUI
{
    /// <summary>
    /// Interaction logic for AddTransactionWindow.xaml
    /// </summary>
    public partial class AddTransactionWindow : Window
    {
        public decimal SumResult { get; private set; }
        public Spending TypeResult { get; private set; }
        public string DescResult { get; private set; }

        public AddTransactionWindow()
        {
            InitializeComponent();
            TypeSelector.ItemsSource = System.Enum.GetValues(typeof(Spending));
            TypeSelector.SelectedIndex = 0;
        }

        private void AddButton(object sender, RoutedEventArgs e)
        {
            if (decimal.TryParse(SumInput.Text, out decimal sum) && TypeSelector.SelectedItem != null)
            {
                SumResult = sum;
                TypeResult = (Spending)TypeSelector.SelectedItem;
                DescResult = DescInput.Text;
                this.DialogResult = true;
            }
        }
    }
}
