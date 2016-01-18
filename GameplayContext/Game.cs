using System;
using System.Collections.Generic;
using SharedKernel;
using GameplayContext.Ports;

namespace GameplayContext
{
    public class Game : IGame
    {
        private const int GameSpeed = 1000;
        private const int NumberSteps = 15;
        private int _sizeOfStep;

        private List<Tile> _columns = new List<Tile>();
        private IGameTimer _timer;
        private IGameDimensions _gameDimensions;

        public Game(IGameTimer timer, IGameDimensions gameDimensions)
        {
            _timer = timer;
            _gameDimensions = gameDimensions;
            FallingTile = new Tile();
            _timer.Tick += GameTimerFired;
            _sizeOfStep = _gameDimensions.GameHeight / NumberSteps;
        }

        public event EventHandler GameChanged;
        public event EventHandler NewTile;

        public Tile FallingTile { get; private set; }

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

        public void StartGame()
        {
            _timer.Start(GameSpeed);
        }

        public List<Tile> GetColumn(int columnNumber)
        {
            return _columns;
        }

        private void GameTimerFired(object sender, EventArgs e)
        {
            if (FallingTile.YPos >= _gameDimensions.GameHeight)
            {
                _columns.Add (FallingTile);
                FallingTile = new Tile ();
                NewTile?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                FallingTile.YPos += _sizeOfStep;
            }

            GameChanged?.Invoke (this, EventArgs.Empty);
        }
    }
}

