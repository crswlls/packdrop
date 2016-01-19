using System;
using SharedKernel;

namespace GameplayContext
{
    public class TileEventArgs : EventArgs
    {
        private Tile _tile;

        public TileEventArgs(Tile tile)
        {
            _tile = tile;
        }

        public Tile Tile
        {
            get
            {
                return _tile;
            }
        }
    }
}

