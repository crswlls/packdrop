using System;
using System.Collections.Generic;
using SharedKernel;
using System.Linq;

namespace GameplayContext
{
    public class Game : IGame
    {
        private const int NumberSteps = 15;
        private int _numberColumns;

        private List<List<Tile>> _columns = new List<List<Tile>>();

        public event EventHandler GameChanged;
        public event EventHandler<TileEventArgs> NewTile;
        public event EventHandler<TileEventArgs> TileFell;
        public event EventHandler<TileEventArgs> TileStopped;
        public event EventHandler<TileEventArgs> TileMoved;
        public event EventHandler GameOver;

        /// <summary>
        /// Exposed for test purposes
        /// </summary>
        /// <value>The number steps to drop.</value>
        public static int NumberStepsToDrop
        {
            get
            {
                return NumberSteps;
            }
        }

        public Tile FallingTile { get; private set; }

        public void StartGame(int numberColumns)
        {
            _numberColumns = numberColumns;
            for (int i = 0; i < _numberColumns;i++)
            {
                _columns.Add(new List<Tile>());
            }
            CreateNewFallingTile();
        }

        public void MoveRight()
        {
            if (FallingTile.XPos < _numberColumns - 1)
            {
                FallingTile.XPos++;
                TileMoved?.Invoke(this, new TileEventArgs(FallingTile));
            }
        }

        public void MoveLeft()
        {
            if (FallingTile.XPos > 0)
            {
                FallingTile.XPos--;
                TileMoved?.Invoke(this, new TileEventArgs(FallingTile));
            }
        }

        public List<Tile> GetColumn(int columnNumber)
        {
            return _columns[columnNumber];
        }

        public void Continue()
        {
            if (IsNewTileRequired)
            {
                TileStopped?.Invoke(this, new TileEventArgs(FallingTile));

                if (IsGameOver)
                {
                    GameOver?.Invoke(this, EventArgs.Empty);
                    return;
                }

                CreateNewFallingTile();
                NewTile?.Invoke(this, new TileEventArgs(FallingTile));
            }
            else
            {
                FallingTile.YPos++;
                TileFell?.Invoke(this, new TileEventArgs(FallingTile));
            }
        }

        private bool IsNewTileRequired
        {
            get
            {
                return _columns[FallingTile.XPos].Count + FallingTile.YPos >= NumberSteps;
            }
        }

        private void CreateNewFallingTile()
        {
            if (FallingTile != null)
            {
                FallingTile.IsFalling = false;
                _columns[FallingTile.XPos].Add(FallingTile);
            }

            FallingTile = new Tile { IsFalling = true, XPos = 4, YPos = 0 };
        }

        private bool IsGameOver
        {
            get
            {
                return _columns.Any(x => x.Count > NumberSteps);
            }
        }
    }
}

