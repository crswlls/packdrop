using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public interface IGame
    {
        void StartGame();
        void Continue();
        Tile FallingTile { get; }
        List<Tile> GetColumn(int columnNumber);
        event EventHandler GameChanged;
        event EventHandler GameOver;
        event EventHandler<TileEventArgs> NewTile;
        event EventHandler<TileEventArgs> TileFell;
        event EventHandler<TileEventArgs> TileStopped;
    }
}

