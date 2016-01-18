using System;
using System.Collections.Generic;
using SharedKernel;

namespace GameplayContext
{
    public class Game : IGame
    {
        private List<Tile> _columns = new List<Tile>();
        private IGameTimer _timer;

        public Game(IGameTimer timer)
        {
            _timer = timer;
            _columns.Add (new Tile ());
            _timer.Tick += (sender, e) => {
                _columns[0].YPos += 1;
                GameChanged?.Invoke(this, EventArgs.Empty);
            };
        }

        public void StartGame()
        {
            _timer.Start();
        }

        public List<Tile> GetColumn(int columnNumber)
        {
            return _columns;
        }

        public event EventHandler GameChanged;
    }
}

