using System;
using ViewModels;

namespace IntegrationTests
{
    public class DispatcherStub : IDispatcher
    {
        #region IDispatcher implementation
        public void RunOnUiThread (Action action)
        {
            action();
        }
        #endregion
    }
}

