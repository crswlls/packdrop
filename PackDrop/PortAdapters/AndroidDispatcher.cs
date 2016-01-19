using System;
using ViewModels;
using Android.OS;

namespace PackDrop
{
    public class AndroidDispatcher : IDispatcher
    {
        public void RunOnUiThread (Action action)
        {
            new Handler(Looper.MainLooper).Post(action);
        }
    }
}

