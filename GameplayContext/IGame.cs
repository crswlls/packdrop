using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public interface IGame
    {
        void StartGame();
        List<Tile> GetColumn(int columnNumber);
        event EventHandler GameChanged;
    }
}

