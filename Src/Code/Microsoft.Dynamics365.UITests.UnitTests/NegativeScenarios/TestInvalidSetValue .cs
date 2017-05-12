using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class TestInvalidSetValue
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void InvalidSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);

                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                xrmBrowser.CommandBar.ClickCommand("New");

                xrmBrowser.Entity.SetValue("subject1", "Test API to Create leads");

            }
        }

        [TestMethod]
        public void InvalidOptionSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                xrmBrowser.CommandBar.ClickCommand("New");

                xrmBrowser.Entity.SetValue(new OptionSet { Name = "Invalidcontactmethodcode", Value = "Email" });

            }
        }

        [TestMethod]
        public void InvalidOpenLookupSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

                xrmBrowser.CommandBar.ClickCommand("New");

                xrmBrowser.Entity.SetValue(new Lookup { Name = "prrimarycontactid", Value = "Rene Valdes (sample)" });

            }
        }

    }
}
