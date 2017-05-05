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
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.GetGridItems();
                Thread.Sleep(1000);

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
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.OpenRecord(0);

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
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.Sort("Account Name");
               
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestSelectRecord()
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

                Thread.Sleep(200);

                xrmBrowser.Grid.SelectRecord(1);
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void TestSelectAllRecords()
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


                xrmBrowser.Grid.SelectAllRecords();
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
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.FilterByLetter('A');
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void FilterGridByAll()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.FilterByAll();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void EnableFilter()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.EnableFilter();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void GridNextPage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("All Leads");

                xrmBrowser.Grid.NextPage();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void GridPreviousPage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.PreviousPage();

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void GridFirstPage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");

                xrmBrowser.Grid.FirstPage();
                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void OpenChart()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.OpenChart();

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void SwitchChart()
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
                xrmBrowser.Grid.OpenChart();
                xrmBrowser.Grid.SwitchChart("Accounts by Owner");

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void RefreshGrid()
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
                xrmBrowser.Grid.Refresh();

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void CloseChart()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.CloseChart();

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void QuickFindSearch()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");

                xrmBrowser.Grid.Search("Test"):

                //This is for Quick Create and method was named wrong. 
                //xrmBrowser.Grid.SwitchToQuickCreateFrame();

                Thread.Sleep(10000);

            }
        }

        [TestMethod]
        public void PinDefaultView()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.Grid.SwitchView("Open Leads");
                xrmBrowser.Grid.Pin();

                Thread.Sleep(10000);
            }
        }
    }
}