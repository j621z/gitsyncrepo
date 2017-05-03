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
    public class Entity
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void OpenEntity()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));
                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void NavigateUp()
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
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                xrmBrowser.Grid.OpenRecord(1);
                xrmBrowser.Entity.NavigateUp();
                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void NavigateDown()
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
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                xrmBrowser.Grid.OpenRecord(0);
                xrmBrowser.Entity.NavigateDown();
                Thread.Sleep(5000);
            }
        }
        [TestMethod]
        public void CollapseTab()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.SelectTab("Summary");
                Thread.Sleep(1000);
            }
        }

        [TestMethod]
        public void PopOutForm()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));
                xrmBrowser.Entity.Popout();

                Thread.Sleep(10000);
            }
        }

        [TestMethod]
        public void OpenLookup()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.SelectLookup("primarycontactid", "Rene Valdes (sample)");
                xrmBrowser.Entity.SelectLookup("primarycontactid", 0);

            }
        }

        [TestMethod]
        public void SaveEntity()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));
                xrmBrowser.Entity.Save();

                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void CloseEntity()
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

                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.CloseEntity();

                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void ExpandTab()
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
                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.CollapseTab("Summary");
                xrmBrowser.Entity.ExpandTab("Summary");

                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void SelectTab()
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
                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.SelectTab("Summary");
                xrmBrowser.Entity.SelectTab("Details");

                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void SelectForm()
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
                xrmBrowser.Entity.OpenEntity("account", Guid.Parse("BD8AC246-2416-E711-8104-FC15B4282DF4"));

                xrmBrowser.Entity.SelectForm("Details");

                Thread.Sleep(5000);
            }
        }
    }
}