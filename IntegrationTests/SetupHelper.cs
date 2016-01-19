using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Views;
using GameplayContext;
using GameplayContext.Ports;
using ViewModels;

namespace IntegrationTests
{
    public class SetupHelper
    {
        private static ViewModelLocator _locator;
        private static NavigationServiceStub _navigationService = new NavigationServiceStub();

        public static GameTimerStub GameTimer
        {
            get
            {
                return SimpleIoc.Default.GetInstance<IGameTimer>() as GameTimerStub;
            }
        }

        public static NavigationServiceStub NavigationService
        {
            get
            {
                return SimpleIoc.Default.GetInstance<INavigationService>() as NavigationServiceStub;
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
            SimpleIoc.Default.Reset();
            if (_locator != null)
            {
                ViewModelLocator.Reset();
            }

            SimpleIoc.Default.Register<INavigationService>(() => _navigationService);
            SimpleIoc.Default.Register<IDispatcher, DispatcherStub>();
            SimpleIoc.Default.Register<IGame, Game>();
            SimpleIoc.Default.Register<IGameTimer, GameTimerStub>();
            SimpleIoc.Default.Register<IGameDimensions, GameDimensionsStub>();
            _locator = new ViewModelLocator();
        }
    }
}