using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.EndUserScenerios
{
    [TestClass]
    public class QuickCreateAccount
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestQuickCreateNewAccount()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.QuickCreate("Account");

                xrmBrowser.ThinkTime(2000);

                xrmBrowser.QuickCreate.SetValue("name", "Test API Account");
                xrmBrowser.QuickCreate.SetValue("telephone1", "555-555-5555");
                xrmBrowser.QuickCreate.SelectLookup("primarycontactid", 0);
                xrmBrowser.QuickCreate.Save();

                xrmBrowser.ThinkTime(2000);
            }
        }
    }
}