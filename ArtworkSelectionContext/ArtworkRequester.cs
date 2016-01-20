using System;
using MixRadio;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ArtworkSelectionContext
{
    public class ArtworkRequester : IArtworkRequester
    {
        private List<Uri> _artwork;

        public List<Uri> Artwork { get { return _artwork; } }

        public async Task<bool> GetArtwork()
        {
            var musicClient = new MusicClient(MixRadioApiConstants.MixRadioClientId);
            var artists = await musicClient.SearchArtistsAsync("David Bowie");
            var products = await musicClient.GetArtistProductsAsync(artists[0].Id, MixRadio.Types.Category.Album, itemsPerPage: 50);
            _artwork = products.Result.Select(x => x.Thumb320Uri).OrderBy(x => Guid.NewGuid()).Take(7).ToList();
            return true;
        }
    }
}

