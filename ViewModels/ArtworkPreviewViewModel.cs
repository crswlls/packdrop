using System;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ArtworkSelectionContext;
using GalaSoft.MvvmLight.Views;
using GalaSoft.MvvmLight.Command;

namespace ViewModels
{
    public class ArtworkPreviewViewModel : ViewModelBase
    {
        private INavigationService _navService;
        private RelayCommand _goToGameCommand;

        public ObservableCollection<Uri> ImageUris { get; set; }

        public ArtworkPreviewViewModel(INavigationService navService)
        {
            _navService = navService;
            ImageUris = new ObservableCollection<Uri>();
        }

        public async Task InitAsync()
        {
            var uris = await new ArtworkRequester().GetArtwork();
            foreach (var imageUri in uris)
            {
                ImageUris.Add(imageUri);
            }
        }

        public RelayCommand GoToGameCommand
        {
            get
            {
                return _goToGameCommand ?? (_goToGameCommand = new RelayCommand(() => _navService.NavigateTo(nameof(InGameViewModel))));
            }
        }
    }
}

