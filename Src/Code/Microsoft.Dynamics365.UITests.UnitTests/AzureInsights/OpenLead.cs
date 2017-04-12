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
    public class OpenLeadforAzure
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private readonly string _azureKey = System.Configuration.ConfigurationManager.AppSettings["AzureKey"].ToString();

        [TestMethod]
        public void OpenActiveLead()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                Thread.Sleep(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                Thread.Sleep(2000);
                xrmBrowser.Grid.SwitchView("All Leads");

                Thread.Sleep(1000);
                xrmBrowser.Grid.OpenGridRow(0);

                var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
                telemetry.InstrumentationKey = _azureKey;


                foreach (var result in xrmBrowser.CommandExecutions)
                {
                    
                    var properties = new Dictionary<string, string>();
                    var metrics = new Dictionary<string, double>();

                    metrics.Add("ThinkTime", result.ThinkTime);
                    metrics.Add("TransitionTime", result.TransitionTime.TotalMilliseconds);
                    metrics.Add("ExecutionTime", result.ExecutionTime.TotalMilliseconds);
                    metrics.Add("ExecutionAttempts", result.ExecutionAttempts);

                    telemetry.TrackEvent(result.CommandName, properties, metrics);
                  
                }

                telemetry.Flush();
            }
        }
    }
}