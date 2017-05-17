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
    public class RecordWall
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());



        [TestMethod]
        public void SelectTab()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void AddPost()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Posts);
                xrmBrowser.ActivityFeed.AddPost("Test Add Post");
                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void AddActivity()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.AddPhoneCall("Test Phone call Description",false);
                Thread.Sleep(5000);
            }
        }

        [TestMethod]
        public void AddNote()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Notes);
                xrmBrowser.ActivityFeed.AddNote("Test Add Note");
                Thread.Sleep(5000);
            }
        }
    }
}