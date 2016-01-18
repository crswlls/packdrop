using System;
using GameplayContext.Ports;

namespace IntegrationTests
{
    public class GameTimerStub : IGameTimer
    {
        public event EventHandler Tick;

        public void Start (int gameSpeed)
        {
        }

        public void DoTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}

