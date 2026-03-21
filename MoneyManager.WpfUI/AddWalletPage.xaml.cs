using System.Windows.Controls;
using MoneyManager.WpfUI.ViewModels;

namespace MoneyManager.WpfUI
{
    public partial class AddWalletPage : Page
    {
        public AddWalletPage(AddWalletPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}