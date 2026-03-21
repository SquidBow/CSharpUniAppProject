using System.Windows;
using System.Windows.Controls;

namespace MoneyManager.WpfUI
{
    public partial class MainWindow : Window
    {
        public MainWindow(Services.INavigationService navService, WalletsPage walletsPage)
        {
            InitializeComponent();
            var nav = (Services.NavigationService)navService;
            var field = typeof(Services.NavigationService).GetField("frame", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            Content = (Frame)field.GetValue(nav);

            navService.NavigateTo(walletsPage);
        }
    }
}
