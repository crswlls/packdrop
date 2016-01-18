using System;

namespace GameplayContext
{
    public interface IGameTimer
    {
        event EventHandler Tick;
        void Start();

    }
}

