using System;
using GalaSoft.MvvmLight;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using ArtworkSelectionContext;

namespace ViewModels
{
    public class ArtworkPreviewViewModel : ViewModelBase
    {
        public ObservableCollection<Uri> ImageUris { get; set; }

        public ArtworkPreviewViewModel()
        {
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
    }
}

