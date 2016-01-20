using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;

namespace ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private RelayCommand _launchCommand;

        public MainViewModel (INavigationService navService)
        {
            _navService = navService;   
        }

        public RelayCommand LaunchCommand
        {
            get
            {
                return _launchCommand ?? (_launchCommand = new RelayCommand(() => _navService.NavigateTo(nameof(ArtworkPreviewViewModel))));
            }
        }
    }
}

