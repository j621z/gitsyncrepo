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
    public class Views
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestSwitchView()
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
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");

            }
        }


        [TestMethod]
        public void TestGetGridItems()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.GetGridItems();

            }
        }

        [TestMethod]
        public void TestOpenGridRecord()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.OpenGridRecord(0);

            }
        }

        [TestMethod]
        public void TestSortGridRow()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.Sort("Account Name");
               
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestSelectGridRecord()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");

                Thread.Sleep(200);

                xrmBrowser.Grid.SelectGridRecord(1);
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestFilterGridByLetter()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.FilterByLetter('A');
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestFilterGridByAll()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.FilterByAll();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestOpenFilter()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.OpenFilter();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestGridNextPage()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.NextPage();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestGridPreviousPage()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.PreviousPage();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestGridFirstPage()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.FirstPage();
                Thread.Sleep(10000);

            }
        }
    }
}