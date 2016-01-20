using ViewModels;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Ioc;
using GameplayContext;
using GameplayContext.Ports;
using PackDrop.PortAdapters;

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
                    nav.Configure(nameof(GameOverViewModel), typeof(GameOverActivity));
                    nav.Configure(nameof(ArtworkPreviewViewModel), typeof(ArtworkPreviewActivity));

                    SimpleIoc.Default.Register<IGameDimensions, GameDimensions>();
                    SimpleIoc.Default.Register<IDispatcher, AndroidDispatcher>();
                    SimpleIoc.Default.Register<IGameTimer, GameTimer>();
                    SimpleIoc.Default.Register<INavigationService> (() => nav);
                    SimpleIoc.Default.Register<IGame, Game> ();
                    SimpleIoc.Default.Register<IDialogService, DialogService> ();

                    _locator = new ViewModelLocator ();
                }

                return _locator;
            }
        }
    }
}

