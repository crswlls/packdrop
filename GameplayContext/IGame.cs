using System;
using System.Collections.Generic;
using SharedKernel;
using System.Collections.ObjectModel;

namespace GameplayContext
{
    public interface IGame
    {
        void StartGame(List<Uri> images, List<ObservableCollection<Tile>> columns);
        void Continue();
        void MoveLeft();
        void MoveRight();
        void Drop ();
        Tile FallingTile { get; }
        event EventHandler GameOver;
        event EventHandler<TileEventArgs> NewTile;
        event EventHandler<TileEventArgs> TileFell;
        event EventHandler<TileEventArgs> TileStopped;
    }
}

