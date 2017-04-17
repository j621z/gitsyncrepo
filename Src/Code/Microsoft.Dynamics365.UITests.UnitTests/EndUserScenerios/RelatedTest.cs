using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;
using System.Drawing.Imaging;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class RelatedTest
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void AccountRelated()
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
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                             
                Thread.Sleep(3000);
                xrmBrowser.Grid.OpenGridRecord(0);
                xrmBrowser.Navigation.OpenRelated("Contacts");
                xrmBrowser.Related.Sort("createdon");

                xrmBrowser.Related.SwitchView("Active Cases");

                xrmBrowser.Related.Search("P");
                xrmBrowser.CommandBar.ClickCommand("ADD NEW CONTACT");
                //xrmBrowser.Related.OpenGridRecord(0);
            }
        }
    }
}