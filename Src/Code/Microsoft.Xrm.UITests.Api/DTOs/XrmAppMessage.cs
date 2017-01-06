using System;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class XrmAppMessage
    {
        internal XrmAppMessage(XrmAppMessageBarPage page)
        {
            _page = page;
        }

        private XrmAppMessageBarPage _page;

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