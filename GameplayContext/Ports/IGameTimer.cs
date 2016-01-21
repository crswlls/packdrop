using System;

namespace GameplayContext.Ports
{
    public interface IGameTimer
    {
        event EventHandler Tick;
        void Start(int gameSpeed);
        void Stop();
        void UpdateInterval(int newSpeed);
    }
}

