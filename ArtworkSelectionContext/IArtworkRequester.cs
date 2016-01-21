using System;
using MixRadio;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ArtworkSelectionContext
{
	public interface IArtworkRequester
	{
        List<Uri> Artwork { get; }
        Task<bool> GetArtwork(string searchTerm, int numberItems);
	}   
}

