using System;
using System.Collections.Generic;
using SharedKernel;
using System.Linq;
using System.Collections.ObjectModel;

namespace GameplayContext
{
    public class Game : IGame
    {
        private const int NumberSteps = 10;
        private int _numberColumns;

        private List<ObservableCollection<Tile>> _columns;
        private List<Uri> _images = new List<Uri>();

        public event EventHandler GameChanged;
        public event EventHandler<TileEventArgs> NewTile;
        public event EventHandler<TileEventArgs> TileFell;
        public event EventHandler<TileEventArgs> TileStopped;
        public event EventHandler<TileEventArgs> TileMoved;
        public event EventHandler<ScoreArgs> Scored;
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

        public int Score { get; private set; }

        public int SpeedLevel { get; private set; }

        public void StartGame(List<Uri> images, List<ObservableCollection<Tile>> columns)
        {
            foreach (var column in columns)
            {
                column.Clear();
            }
            Score = 0;
            _columns = columns;
            _images = images;
            _numberColumns = columns.Count();
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

        public void Drop ()
        {
            FallingTile.YPos = NumberSteps - _columns[FallingTile.XPos].Count;
        }

        public void Continue()
        {
            if (IsNewTileRequired)
            {
                TileStopped?.Invoke(this, new TileEventArgs(FallingTile));
                if (FallingTile != null)
                {
                    FallingTile.IsFalling = false;
                    _columns[FallingTile.XPos].Add(FallingTile);
                }

                var lastScore = 0;
                var scoreChecker = new ScoreChecker();
                do
                {
                    lastScore = scoreChecker.CheckScoreAndUpdate(_columns);
                    Score += lastScore;
                    if (lastScore > 0)
                    {
                        Scored?.Invoke(this, new ScoreArgs(scoreChecker.RemovedItems));
                    }
                }
                while (lastScore > 0);

                if (IsGameOver)
                {
                    GameOver?.Invoke(this, EventArgs.Empty);
                    return;
                }

                CreateNewFallingTile();
                SpeedLevel++;
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
            FallingTile = new Tile { IsFalling = true, XPos = 4, YPos = 0, ImageId = _images.OrderBy(x => Guid.NewGuid()).First().AbsoluteUri };
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

