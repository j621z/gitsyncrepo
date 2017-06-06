﻿using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.NegativeScenarios.GlobalSearch
{
    [TestClass]
    public class InvalidOpenRecord
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestInvalidSearchOpenRecord()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("");
                xrmBrowser.GlobalSearch.Search("Contsoso");
                xrmBrowser.GlobalSearch.OpenRecord("Accounts", 0);
                xrmBrowser.ThinkTime(4000);


            }
        }

        [TestMethod]
        public void TestInvalidOpenRecord()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("");
                xrmBrowser.GlobalSearch.Search("Contoso");
                xrmBrowser.GlobalSearch.OpenRecord("Account", 0);
                xrmBrowser.ThinkTime(4000);


            }
        }
    }
}