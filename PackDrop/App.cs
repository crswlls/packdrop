﻿using ViewModels;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;

namespace PackDrop
{
    public static class App
    {
        private static ViewModelLocator _locator;

        public static ViewModelLocator Locator
        {
            get
            {
                if (_locator == null)
                {
                    var nav = new NavigationService ();
                    nav.Configure(nameof(InGameViewModel), typeof(InGameActivity));

                    SimpleIoc.Default.Register<INavigationService> (() => nav);
                    SimpleIoc.Default.Register<IDialogService, DialogService> ();

                    _locator = new ViewModelLocator ();
                }

                return _locator;
            }
        }
    }
}
