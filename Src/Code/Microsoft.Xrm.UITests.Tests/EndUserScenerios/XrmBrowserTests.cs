using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Xrm.UITests.Api;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Xrm.UITests.Tests
{
    [TestClass]
    public class XrmBrowserTests
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        private readonly SecureString _onPremUsername = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _onPremPassword = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _onPremUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());


        [TestMethod]
        public void TestOnPremLogin()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.IE,
                PrivateMode = true
            }))
            {
                xrmBrowser.LoginPage.Login(_onPremUri, _onPremUsername, _onPremPassword);

            }
        }

        [TestMethod]
        public void TestOpenLeadsView()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                var perf = xrmBrowser.PerformanceCenter;

                if (!perf.IsEnabled)
                    perf.IsEnabled = true;

                Thread.Sleep(500);

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                Thread.Sleep(4000);

                //var views = xrmBrowser.OpenViewPicker();
                xrmBrowser.Grid.SwitchView("All Leads");
                Thread.Sleep(1000);

                var items = xrmBrowser.Grid.GetGridItems().Value;

                foreach (var item in items)
                {
                    xrmBrowser.Entity.OpenEntity(item.Url);

                    perf.ToggleVisibility();
                    Thread.Sleep(1500);
                    perf.ToggleVisibility();
                }
            }
        }
      
    }
   
}