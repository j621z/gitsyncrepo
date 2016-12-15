using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.UITests.Api;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Xrm.UITests.Tests
{
    [TestClass]
    public class QuickCreateAccountTests
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void QuickCreateNewAccount()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);

                xrmBrowser.Navigation.QuickCreate("Account");

                Thread.Sleep(2000);

                xrmBrowser.QuickCreate.SetValue("name", "Test API Account");
                xrmBrowser.QuickCreate.SetValue("telephone1", "555-555-5555");
                xrmBrowser.QuickCreate.SelectLookup("primarycontactid", 0);
                xrmBrowser.QuickCreate.Save();

                Thread.Sleep(2000);
            }
        }
    }
}