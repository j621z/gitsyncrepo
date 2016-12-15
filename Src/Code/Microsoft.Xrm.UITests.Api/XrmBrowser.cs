using OpenQA.Selenium;
using System;
using Microsoft.Xrm.UITests.Api.Pages;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Xrm.UITests.Api
{
    public class XrmBrowser
        : InteractiveBrowser
    {
        #region Constructor(s)

        internal XrmBrowser(IWebDriver driver) : base(driver)
        {
        }

        public XrmBrowser(BrowserType type) : base(type)
        {
        }

        public XrmBrowser(BrowserOptions options) : base(options)
        {
            
        }

        #endregion Constructor(s)

        #region Login

        public LoginPage LoginPage
        {
            get
            {
                return this.GetPage<LoginPage>();
            }
        }

        public void GoToXrmUri(Uri xrmUri)
        {
            this.Driver.Navigate().GoToUrl(xrmUri);
            this.Driver.WaitForPageToLoad();
        }

        #endregion Login

        #region Navigation

        public XrmNavigationPage Navigation
        {
            get
            {
                return this.GetPage<XrmNavigationPage>();
            }
        }

        #endregion Navigation

        #region CommandBar

        public XrmCommandBarPage CommandBar
        {
            get
            {
                return this.GetPage<XrmCommandBarPage>();
            }
        }

        #endregion CommandBar

        #region Performance Markers

        public XrmPerformanceCenterPage PerformanceCenter
        {
            get
            {
                return this.GetPage<XrmPerformanceCenterPage>();
            }
        }

        #endregion Performance Markers

        #region Views

        public XrmGridPage Grid
        {
            get
            {
                return this.GetPage<XrmGridPage>();
            }
        }

        #endregion Views

        #region Forms

        public XrmEntityPage Entity
        {
            get
            {
                return this.GetPage<XrmEntityPage>();
            }
        }

        #endregion Forms

        #region Related

        public XrmRelatedGridPage Related
        {
            get
            {
                return this.GetPage<XrmRelatedGridPage>();
            }
        }

        #endregion Related

        #region QuickCreate

        public XrmQuickCreatePage QuickCreate
        {
            get
            {
                return this.GetPage<XrmQuickCreatePage>();
            }
        }

        #endregion QuickCreate

        #region LookupPage

        public XrmLookupPage Lookup
        {
            get
            {
                return this.GetPage<XrmLookupPage>();
            }
        }

        #endregion LookupPage

        #region Guided Help

        public XrmGuidedHelpPage GuidedHelp
        {
            get
            {
                return this.GetPage<XrmGuidedHelpPage>();
            }
        }

        #endregion Guided Help

        #region Recorder

        public XrmRecorderPage Recorder
        {
            get
            {
                return this.GetPage<XrmRecorderPage>();
            }
        }

        #endregion Recorder

    }
   

}