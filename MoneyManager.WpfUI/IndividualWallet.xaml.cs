using System.Windows.Controls;
using MoneyManager.Models;
using MoneyManager.WpfUI.ViewModels;
using MoneyManager.WpfUI.Services;

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
    }
}