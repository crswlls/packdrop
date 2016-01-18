using System;
using ViewModels;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GameplayContext;

namespace IntegrationTests
{
    public static class SetupHelper
    {
        private static ViewModelLocator _locator;
        private static GameTimerStub _gameTimer = new GameTimerStub();

        public static GameTimerStub GameTimer
        {
            get
            {
                return _gameTimer;
            }
        }

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
            SimpleIoc.Default.Register<IGame, Game>();
            SimpleIoc.Default.Register<IGameTimer> (() => _gameTimer);
            _locator = new ViewModelLocator ();
        }
    }
}

