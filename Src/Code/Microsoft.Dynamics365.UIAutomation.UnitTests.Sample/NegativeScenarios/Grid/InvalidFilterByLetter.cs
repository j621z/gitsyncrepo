﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.NegativeScenarios.Grid
{
    [TestClass]
    public class InvalidFilterByLetter
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestFilterGridByLetter()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");
                xrmBrowser.Grid.SwitchView("Active Accounts");
                xrmBrowser.Grid.FilterByLetter('&');
                xrmBrowser.ThinkTime(5000);

            }
        }

    }
}