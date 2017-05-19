using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UITests.UnitTests.Sample
{
    [TestClass]
    public class CreateContact
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void CreateNewContact()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                Thread.Sleep(1000);
                xrmBrowser.CommandBar.ClickCommand("New");

                Thread.Sleep(5000);

                var fields = new List<Field>
                {
                    new Field() {Id = "firstname", Value = "Test"},
                    new Field() {Id = "lastname", Value = "Contact"}
                };
                xrmBrowser.Entity.SetValue(new CompositeControl() {Id = "fullname", Fields = fields});
                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
                xrmBrowser.Entity.SetValue(new OptionSet {Name = "preferredcontactmethodcode", Value = "Email"});

                xrmBrowser.CommandBar.ClickCommand("Save");
                
            }
        }
    }
}