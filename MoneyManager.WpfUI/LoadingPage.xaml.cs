using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using MoneyManager.Models;
using MoneyManager.WpfUI.Services;

namespace MoneyManager.WpfUI
{
    public partial class LoadingPage : Page
    {
        private readonly IAppService service;
        private readonly INavigationService navService;
        private readonly WalletsPage walletsPage;

        public LoadingPage(IAppService service, INavigationService navService, WalletsPage
walletsPage)
        {
            InitializeComponent();
            this.service = service;
            this.navService = navService;
            this.walletsPage = walletsPage;

            this.Loaded += OnPageLoaded;
        }

        private async void OnPageLoaded(object sender, RoutedEventArgs e)
        {
            await service.LoadData();

            await Task.Delay(1000);

            navService.NavigateTo(walletsPage);
        }
    }
}