﻿using OpenQA.Selenium;
using System;
using Microsoft.Dynamics365.UITests.Api.Pages;
using OpenQA.Selenium.Support.Events;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    /// <summary>
    /// Provides API methods to simulate user interaction with the Dynamics 365 application. 
    /// </summary>
    /// <seealso cref="Microsoft.Dynamics365.UITests.Browser.InteractiveBrowser" />
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

        #region Notifications

        public XrmNotficationPage Notifications
        {
            get
            {
                return this.GetPage<XrmNotficationPage>();
            }
        }

        #endregion Notifications

        #region Business Process Flow

        public XrmBusinessProcessFlow BusinessProcessFlow
        {
            get
            {
                return this.GetPage<XrmBusinessProcessFlow>();
            }
        }

        #endregion Business Process Flow

        #region Dialog

        public XrmDialogPage Dialogs
        {
            get
            {
                return this.GetPage<XrmDialogPage>();
            }
        }

        #endregion Dialog

        #region Document

        public XrmDocumentPage Document
        {
            get
            {
                return this.GetPage<XrmDocumentPage>();
            }
        }

        #endregion Document

    }


}