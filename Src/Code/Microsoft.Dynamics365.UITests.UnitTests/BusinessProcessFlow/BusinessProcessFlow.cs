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
    public class BusinessProcessFlow
    {
       
        private readonly SecureString  _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestBusinessProcessFlow()
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
                
                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

                xrmBrowser.BusinessProcessFlow.PreviousStage();

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowNextStage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowPreviousStage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

                xrmBrowser.BusinessProcessFlow.PreviousStage();

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowHide()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

            }
        }

        [TestMethod]
        public void TestBusinessProcessFlowSelectStage()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);
            }
        }


        [TestMethod]
        public void TestBusinessProcessFlowSetActive()
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

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);

                xrmBrowser.BusinessProcessFlow.SetActive();
            }
        }

    }
}