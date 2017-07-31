using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.Entity
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
                xrmBrowser.ActivityFeed.AddPhoneCall("Test Phone call Description", false);
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
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.AddAppointment();
                xrmBrowser.Entity.SetValue("subject", "Add Appointment");
                xrmBrowser.CommandBar.ClickCommand("Save");
                xrmBrowser.ThinkTime(2000);
            }
        }

        [TestMethod]
        public void TestAddEmail()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.AddEmail();
                xrmBrowser.Entity.SetValue("subject", "Test Mail");
                xrmBrowser.ThinkTime(2000);
            }
        }

        [TestMethod]
        public void TestAddTask()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.AddTask("Schedule an appointment", "Capture preliminary customer and product information.", DateTime.Now, new OptionSet { Name = "quickCreateActivity4212controlId_prioritycode_d", Value = "Normal" });
                xrmBrowser.ThinkTime(4000);
            }
        }

        [TestMethod]
        public void TestFilterActivitiesByStatus()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Entity.OpenEntity(TestSettings.AccountLogicalName, Guid.Parse(TestSettings.AccountId));
                xrmBrowser.ActivityFeed.SelectTab(Api.Pages.XrmActivityFeedPage.Tab.Activities);
                xrmBrowser.ActivityFeed.FilterActivitiesByStatus(Api.Pages.XrmActivityFeedPage.Status.Overdue);
                xrmBrowser.ThinkTime(3000);
            }
        }
    }
}