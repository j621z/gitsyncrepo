using System;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmAppNotification
    {
        internal XrmAppNotification(XrmNotficationPage page)
        {
            _page = page;
        }

        private XrmNotficationPage _page;

        public Int32 Index { get; internal set; }
        public string Title { get; internal set; }
        public string Message { get; internal set; }
        public string DismissButtonText { get; internal set; }

        public void Dismiss()
        {
            _page.Dismiss(this);
        }
    }
}