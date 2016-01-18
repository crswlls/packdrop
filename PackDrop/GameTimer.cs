using System;
using System.Timers;

namespace GameplayContext
{
    public class GameTimer : IGameTimer
    {
        private Timer _timer = new Timer();

        public GameTimer()
        {
            _timer.Elapsed += (sender, e) => Tick?.Invoke(sender, e);
            _timer.Interval = 1000;
        }

        public event EventHandler Tick;
        public void Start ()
        {
            _timer.Start();
        }
    }
}

