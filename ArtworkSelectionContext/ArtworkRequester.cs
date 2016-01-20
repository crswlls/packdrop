using System;
using MixRadio;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ArtworkSelectionContext
{
    public class ArtworkRequester
    {
        public async Task<List<Uri>> GetArtwork()
        {
            var musicClient = new MusicClient(MixRadioApiConstants.MixRadioClientId);
            var artists = await musicClient.SearchArtistsAsync("David Bowie");
            var products = await musicClient.GetArtistProductsAsync(artists[0].Id, MixRadio.Types.Category.Album, itemsPerPage: 50);
            return products.Result.Select(x => x.Thumb320Uri).ToList();
        }
    }
}

