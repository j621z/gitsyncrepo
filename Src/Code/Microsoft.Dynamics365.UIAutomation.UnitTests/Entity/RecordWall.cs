﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests
{
    [TestClass]
    public class RecordWall
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());



        [TestMethod]
        public void TestSelectTab()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ThinkTime(5000);
            }
        }

        [TestMethod]
        public void TestAddPost()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Posts);
                xrmBrowser.ActivityFeed.AddPost("Test Add Post");
                xrmBrowser.ThinkTime(5000);
            }
        }

        [TestMethod]
        public void TestAddActivity()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.AddPhoneCall("Test Phone call Description",false);
                xrmBrowser.ThinkTime(5000);
            }
        }

        [TestMethod]
        public void TestAddNote()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Notes);
                xrmBrowser.ActivityFeed.AddNote("Test Add Note");
                xrmBrowser.ThinkTime(5000);
            }
        }
        [TestMethod]
        public void TestAddAppointment()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Notes);
                xrmBrowser.ActivityFeed.AddAppointment();
                xrmBrowser.ThinkTime(5000);
            }
        }
    }
}