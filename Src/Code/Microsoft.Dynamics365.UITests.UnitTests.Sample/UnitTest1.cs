using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Browser;
using System.Security;
using Microsoft.Dynamics365.UITests.Api;

namespace Microsoft.Dynamics365.UITests.UnitTests.Sample
{
    [TestClass]
    public class UnitTest1
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestMethod1()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Dashboards");

                //xrmBrowser.Dashboard.SelectDashBoard("Sales Dashboard");

                xrmBrowser.Dashboard.SelectDashBoard("Sales Performance Dashboard");

            }

        }
    }
}
