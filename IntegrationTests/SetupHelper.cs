using System;
using ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;

namespace IntegrationTests
{
    public static class SetupHelper
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                return _locator;
            }
        }

        public static void InitialiseApp()
        {
            var nav = new NavigationServiceStub();

            SimpleIoc.Default.Register<INavigationService> (() => nav);
            _locator = new ViewModelLocator ();
        }
    }
}

