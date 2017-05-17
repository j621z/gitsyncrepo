using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class SetValue
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void SetValues()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                xrmBrowser.Grid.SwitchView("All Leads");

                xrmBrowser.Grid.OpenRecord(0);

                List<Field> fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = "Test"},
                    new Field() {Id = "lastname", Value = "Lead"}
                };
                xrmBrowser.Entity.SetValue("subject", "Test API Lead");
                xrmBrowser.Entity.SetValue(new CompositeControl() { Id = "fullname", Fields = fields });
                xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
                xrmBrowser.Entity.SetValue("description", "Test lead creation with API commands");

            }
        }

        [TestMethod]
        public void SelectOptionSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                Thread.Sleep(5000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });

            }
        }

        [TestMethod]
        public void OpenLookupSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Accounts");

                Thread.Sleep(5000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.Entity.SetValue(new Lookup { Name = "primarycontactid",Value = "Rene Valdes (sample)" });

            }
        }

        [TestMethod]
        public void SelectDateSetValue()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                Thread.Sleep(5000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));

            }
        }
    }
}