using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ArtworkSelectionContext
{
	public interface IArtworkRequester
	{
        List<Uri> Artwork { get; }
        Task<bool> GetArtwork(string searchTerm, int numberItems);
	}   
}

