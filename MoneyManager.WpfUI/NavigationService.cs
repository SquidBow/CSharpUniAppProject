using System.Windows.Controls;

namespace MoneyManager.WpfUI.Services
{
    public interface INavigationService
    {
        void NavigateTo(Page page);
        void GoBack();
    }

    public class NavigationService : INavigationService
    {
        private Frame frame;

        public NavigationService(Frame frame)
        {
            this.frame = frame;
        }

        public void NavigateTo(Page page)
        {
            frame.Navigate(page);
        }

        public void GoBack()
        {
            if (frame.CanGoBack)
            {
                frame.GoBack();
            }
        }
    }
}