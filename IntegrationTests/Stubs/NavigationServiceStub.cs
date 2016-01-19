using GalaSoft.MvvmLight.Views;

namespace IntegrationTests
{
    public class NavigationServiceStub : INavigationService
    {
        private string _lastNav;

        public void GoBack ()
        {
            
        }

        public void NavigateTo (string pageKey)
        {
            _lastNav = pageKey;
        }

        public void NavigateTo (string pageKey, object parameter)
        {
            _lastNav = pageKey;
        }

        public string CurrentPageKey
        {
            get
            {
                return _lastNav;
            }
        }
    }
}

