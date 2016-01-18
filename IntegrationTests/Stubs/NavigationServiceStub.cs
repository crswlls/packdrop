using System;
using GalaSoft.MvvmLight.Views;

namespace IntegrationTests
{
    public class NavigationServiceStub : INavigationService
    {
        public void GoBack ()
        {
            
        }

        public void NavigateTo (string pageKey)
        {
            
        }

        public void NavigateTo (string pageKey, object parameter)
        {
            
        }

        public string CurrentPageKey
        {
            get
            {
                return string.Empty;
            }
        }
    }
}

