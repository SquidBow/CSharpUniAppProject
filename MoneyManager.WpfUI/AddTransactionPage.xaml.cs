using System.Windows.Controls;
using MoneyManager.Models;
using MoneyManager.WpfUI.ViewModels;
using MoneyManager.WpfUI.Services;

namespace MoneyManager.WpfUI
{
    public partial class AddTransactionPage : Page
    {
        public AddTransactionPage(AddTransactionPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}