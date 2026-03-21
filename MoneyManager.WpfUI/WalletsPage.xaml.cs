using System.Windows.Controls;
using MoneyManager.WpfUI.ViewModels;

namespace MoneyManager.WpfUI
{
    public partial class WalletsPage : Page
    {
        public WalletsPage(WalletsPageViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;

            this.Loaded += (s, e) => viewModel.LoadWallets();
        }
    }
}