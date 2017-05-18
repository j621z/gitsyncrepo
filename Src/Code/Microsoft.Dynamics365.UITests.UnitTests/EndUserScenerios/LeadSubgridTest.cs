using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;
using OpenQA.Selenium.Support.Events;
using System.Drawing.Imaging;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class LeadSubgridTest
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestClickSubGridInLead()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);

                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                xrmBrowser.Grid.SwitchView("All Leads");
                
                xrmBrowser.Grid.OpenRecord(0);


                xrmBrowser.Entity.ClickSubgridAddButton("Stakeholders");
                xrmBrowser.Entity.SelectSubgridLookup("Stakeholders", "Alex Wu");
                xrmBrowser.Entity.ClickSubgridAddButton("Stakeholders");
                xrmBrowser.Entity.SelectSubgridLookup("Stakeholders", 3);
                xrmBrowser.Entity.ClickSubgridAddButton("Stakeholders");
                xrmBrowser.Entity.SelectSubgridLookup("Stakeholders", true);
                xrmBrowser.Lookup.SelectItem(0);
                xrmBrowser.Lookup.Select();
                xrmBrowser.Lookup.SelectItem("Alex Wu");
                xrmBrowser.Lookup.Select();
                xrmBrowser.Lookup.Add();
            }
        }
    }
}