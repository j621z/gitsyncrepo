using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Security;
using Microsoft.Dynamics365.UITests.Browser;
using Microsoft.Dynamics365.UITests.Api;
using System.Threading;
using System.Collections.Generic;

namespace Microsoft.Dynamics365.UITests.UnitTests.Sample
{
    [TestClass]
    public class UpdateContact
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestUpdateContact()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                var perf = xrmBrowser.PerformanceCenter;

                if (!perf.IsEnabled)
                    perf.IsEnabled = true;

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("Active Contacts");

                Thread.Sleep(1000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("mobilephone", "555-555-5555");
                xrmBrowser.Entity.SetValue("birthdate", DateTime.Parse("11/1/1980"));
                xrmBrowser.Entity.SetValue(new OptionSet { Name = "preferredcontactmethodcode", Value = "Email" });

                xrmBrowser.Entity.Save();

            }
        }
    }
}
