﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class GlobalSearchXrm
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestGlobalSearch()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("contoso");

                xrmBrowser.GlobalSearch.Search("Contoso");
                xrmBrowser.ThinkTime(4000);


            }
        }

        [TestMethod]
        public void TestOpenRecord()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("");
                xrmBrowser.GlobalSearch.Search("Contoso");
                xrmBrowser.GlobalSearch.OpenRecord("Accounts",0) ;
                xrmBrowser.ThinkTime(4000);


            }
        }

        [TestMethod]
        public void TestFilterWith()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.GlobalSearch("");
                xrmBrowser.GlobalSearch.Search("Contoso");
                xrmBrowser.GlobalSearch.FilterWith("Account");

                xrmBrowser.ThinkTime(4000);


            }
        }
    }
}