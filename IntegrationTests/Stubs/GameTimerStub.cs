using GameplayContext;
using System;

namespace IntegrationTests
{
    public class GameTimerStub : IGameTimer
    {
        public event EventHandler Tick;

        public void Start ()
        {
        }

        public void DoTick()
        {
            Tick?.Invoke(this, EventArgs.Empty);
        }
    }
}

