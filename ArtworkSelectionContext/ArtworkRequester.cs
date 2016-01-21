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

        public async Task<bool> GetArtwork(string searchTerm, int numberItems)
        {
            var musicClient = new MusicClient(MixRadioApiConstants.MixRadioClientId);
            var artists = await musicClient.SearchArtistsAsync(searchTerm);
            if (artists.Succeeded && artists.Count > 0)
            {
                var chosenArtist = artists.FirstOrDefault(x => x.Name.Equals(searchTerm, StringComparison.CurrentCultureIgnoreCase));
                chosenArtist = chosenArtist ?? artists[0];

                var products = await musicClient.GetArtistProductsAsync(chosenArtist.Id, itemsPerPage: 50);
                if (products.Succeeded && products.Count >= numberItems)
                {
                    _artwork = products.Result.Select(x => x.Thumb320Uri).OrderBy(x => Guid.NewGuid()).Take(numberItems).ToList();
                    return true;
                }
            }

            return false;
        }
    }
}

