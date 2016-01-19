using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public interface IGame
    {
        void StartGame(int numberColumns);
        void Continue();
        void MoveLeft();
        void MoveRight();
        void Drop ();
        Tile FallingTile { get; }
        List<Tile> GetColumn(int columnNumber);
        event EventHandler GameChanged;
        event EventHandler GameOver;
        event EventHandler<TileEventArgs> NewTile;
        event EventHandler<TileEventArgs> TileFell;
        event EventHandler<TileEventArgs> TileStopped;
        event EventHandler<TileEventArgs> TileMoved;
    }
}

