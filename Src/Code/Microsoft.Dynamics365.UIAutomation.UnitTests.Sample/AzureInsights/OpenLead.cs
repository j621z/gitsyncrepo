﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;
using OpenQA.Selenium.Support.Events;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample
{
    [TestClass]
    public class OpenLeadforAzure
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());
        private readonly string _azureKey = System.Configuration.ConfigurationManager.AppSettings["AzureKey"].ToString();

        [TestMethod]
        public void TestOpenActiveLead()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                
                xrmBrowser.Grid.SwitchView("All Leads");
                
                xrmBrowser.Grid.OpenRecord(0);


                var telemetry = new Microsoft.ApplicationInsights.TelemetryClient();
                telemetry.InstrumentationKey = _azureKey;


                foreach (ICommandResult result in xrmBrowser.CommandResults)
                {
                    
                    var properties = new Dictionary<string, string>();
                    var metrics = new Dictionary<string, double>();

                    properties.Add("StartTime", result.StartTime.Value.ToLongDateString());
                    properties.Add("EndTime", result.StopTime.Value.ToLongDateString());

                    metrics.Add("ThinkTime", result.ThinkTime);
                    metrics.Add("TransitionTime", result.TransitionTime);
                    metrics.Add("ExecutionTime", result.ExecutionTime);
                    metrics.Add("ExecutionAttempts", result.ExecutionAttempts);

                    telemetry.TrackEvent(result.CommandName, properties, metrics);
                                      
                }

                telemetry.Flush();
            }
        }
    }
}