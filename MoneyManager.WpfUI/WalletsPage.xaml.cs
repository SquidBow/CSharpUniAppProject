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

        private void UpdateShownWallets(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            if (DataContext is WalletsPageViewModel vm)
            {
                vm.UpdateShownWallets(((System.Windows.Controls.TextBox)sender).Text);
            }
        }

        private void SortByName(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is WalletsPageViewModel vm)
                vm.SortBy("Name");
        }

        private void SortByBalance(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is WalletsPageViewModel vm)
                vm.SortBy("Balance");
        }

        private void SortById(object sender, System.Windows.RoutedEventArgs e)
        {
            if (DataContext is WalletsPageViewModel vm)
                vm.SortBy("Id");
        }
    }
}