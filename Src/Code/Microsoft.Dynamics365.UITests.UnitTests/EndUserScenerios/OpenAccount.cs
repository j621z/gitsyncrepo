using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using OpenQA.Selenium.Support.Events;
using System.Drawing.Imaging;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class OpenAccount
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestOpenActiveAccount()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);

                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                
                xrmBrowser.ThinkTime(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

                xrmBrowser.ThinkTime(2000);
                xrmBrowser.Grid.SwitchView("Active Accounts");
                
                xrmBrowser.ThinkTime(1000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.ThinkTime(2000);
                xrmBrowser.Entity.CollapseTab("Summary");

                xrmBrowser.ThinkTime(2000);
                xrmBrowser.Entity.ExpandTab("Summary");

                xrmBrowser.ThinkTime(2000);
                //xrmBrowser.Entity.SelectSection("Details");
            }
        }
    }
}