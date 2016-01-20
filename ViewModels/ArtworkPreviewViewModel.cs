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
        private IArtworkRequester _requester;

        public ObservableCollection<Uri> ImageUris { get; set; }

        public ArtworkPreviewViewModel(INavigationService navService, IArtworkRequester requester)
        {
            _navService = navService;
            _requester = requester;
            ImageUris = new ObservableCollection<Uri>();
        }

        public async Task InitAsync()
        {
            await _requester.GetArtwork();
            foreach (var imageUri in _requester.Artwork)
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

