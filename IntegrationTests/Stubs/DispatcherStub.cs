using System;
using ViewModels;

namespace IntegrationTests
{
    public class DispatcherStub : IDispatcher
    {
        public void RunOnUiThread (Action action)
        {
            action();
        }
    }
}