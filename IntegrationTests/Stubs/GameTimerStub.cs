using System;
using GameplayContext.Ports;

namespace IntegrationTests
{
    public class GameTimerStub : IGameTimer
    {
        public event EventHandler Tick;
        public bool _running;

        public void Start(int gameSpeed)
        {
            _running = true;
        }

        public void DoTick()
        {
            if (_running)
            {
                Tick?.Invoke(this, EventArgs.Empty);
            }
        }

        public void Stop ()
        {
            _running = false;
        }
    }
}

