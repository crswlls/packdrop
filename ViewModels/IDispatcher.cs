using System;

namespace ViewModels
{
    public interface IDispatcher
    {
        void RunOnUiThread(Action action);
    }
}

