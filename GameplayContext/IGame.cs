using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public interface IGame
    {
        void StartGame();
        Tile FallingTile { get; }
        List<Tile> GetColumn(int columnNumber);
        event EventHandler GameChanged;
        event EventHandler NewTile;
    }
}

