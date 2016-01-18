using System;
using System.Timers;
using GameplayContext.Ports;

namespace PackDrop.PortAdapters
{
    public class GameTimer : IGameTimer
    {
        private Timer _timer = new Timer();

        public GameTimer()
        {
            _timer.Elapsed += (sender, e) => Tick?.Invoke(sender, e);

        }

        public event EventHandler Tick;
        public void Start (int gameSpeed)
        {
            _timer.Interval = gameSpeed;
            _timer.Start();
        }
    }
}

