using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class BrowserInitializeEventArgs : EventArgs
    {
        public BrowserInitializeEventArgs(BrowserInitiationSource source)
        {
            this.Source = source;
        }

        public BrowserInitiationSource Source { get; private set; }
    }
}