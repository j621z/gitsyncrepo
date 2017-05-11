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
    public class GetValue
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void GetValueFromOpenActiveLead()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                xrmBrowser.Grid.SwitchView("All Leads");

                xrmBrowser.Grid.OpenRecord(0);

                string subject = xrmBrowser.Entity.GetValue("subject");
                string mobilePhone = xrmBrowser.Entity.GetValue("mobilephone");
            }
        }

        public void GetValueFromCompositeControl()
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
                    new Field() {Id = "firstname"},
                    new Field() {Id = "lastname"}
                };
                string fullName = xrmBrowser.Entity.GetValue(new CompositeControl() { Id = "fullname", Fields = fields });
            }
        }


        [TestMethod]
        public void GetValueFromOptionSet()
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

                string birthDate = xrmBrowser.Entity.GetValue("birthdate");
                string options = xrmBrowser.Entity.GetValue(new OptionSet { Name = "preferredcontactmethodcode"});
                

            }
        }

        [TestMethod]
        public void GetValueFromLookup()
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

                string lookupValue = xrmBrowser.Entity.GetValue(new Lookup { Name = "primarycontactid" });

            }
        }
    }
}