﻿using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public class Game : IGame
    {
        private const int NumberSteps = 15;

        private List<Tile> _columns = new List<Tile>();

        public event EventHandler GameChanged;
        public event EventHandler<TileEventArgs> NewTile;
        public event EventHandler<TileEventArgs> TileFell;
        public event EventHandler<TileEventArgs> TileStopped;
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

        public void StartGame()
        {
            _columns.Clear();
            CreateNewFallingTile();
        }

        public List<Tile> GetColumn(int columnNumber)
        {
            return _columns;
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
                return _columns.Count + FallingTile.YPos >= NumberSteps;
            }
        }

        private void CreateNewFallingTile()
        {
            if (FallingTile != null)
            {
                FallingTile.IsFalling = false;
                _columns.Add(FallingTile);
            }

            FallingTile = new Tile { IsFalling = true, XPos = 0, YPos = 0 };
        }

        private bool IsGameOver
        {
            get
            {
                return _columns.Count > NumberSteps;
            }
        }
    }
}

