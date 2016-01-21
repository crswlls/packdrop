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
        private const int NumberImagesRequired = 7;

        private INavigationService _navService;
        private RelayCommand _goToGameCommand;
        private RelayCommand<string> _searchCommand;
        private IArtworkRequester _requester;
        private bool _haveImages;

        public ObservableCollection<Uri> ImageUris { get; set; }

        public ArtworkPreviewViewModel(INavigationService navService, IArtworkRequester requester)
        {
            _navService = navService;
            _requester = requester;
            ImageUris = new ObservableCollection<Uri>();
        }

        public RelayCommand GoToGameCommand
        {
            get
            {
                return _goToGameCommand ?? (_goToGameCommand = new RelayCommand(() => _navService.NavigateTo(nameof(InGameViewModel)),
                                                                                () => _haveImages));
            }
        }

        public RelayCommand<string> SearchCommand
        {
            get
            {
                return _searchCommand ?? (_searchCommand = new RelayCommand<string>(DoSearch));
            }
        }

        private async void DoSearch(string text)
        {
            ImageUris.Clear();
            if (await _requester.GetArtwork(text, NumberImagesRequired))
            {
                foreach (var imageUri in _requester.Artwork)
                {
                    ImageUris.Add(imageUri);
                    _haveImages = true;
                    GoToGameCommand.RaiseCanExecuteChanged();
                }
            }
        }
    }
}

