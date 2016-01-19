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
            _timer.Elapsed += (sender, args) => Tick?.Invoke(sender, args);

        }

        public event EventHandler Tick;
        public void Start (int gameSpeed)
        {
            _timer.Interval = gameSpeed;
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}

