// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Collections.Generic;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.EndUserScenerios
{
    [TestClass]
    public class XrmBrowserTests
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        private readonly SecureString _onPremUsername = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _onPremPassword = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _onPremUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());


        [TestMethod]
        public void TestOnPremLogin()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_onPremUri, _onPremUsername, _onPremPassword);

            }
        }

        [TestMethod]
        public void TestOpenLeadsView()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                //xrmBrowser.GuidedHelp.CloseGuidedHelp();

                var perf = xrmBrowser.PerformanceCenter;

                if (!perf.IsEnabled)
                    perf.IsEnabled = true;

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                xrmBrowser.ThinkTime(4000);

                //var views = xrmBrowser.OpenViewPicker();
                xrmBrowser.Grid.SwitchView("All Leads");
                xrmBrowser.ThinkTime(1000);

                var items = xrmBrowser.Grid.GetGridItems().Value;

                foreach (var item in items)
                {
                    xrmBrowser.Entity.OpenEntity(item.Url);

                    perf.ToggleVisibility();
                    xrmBrowser.ThinkTime(1500);
                    perf.ToggleVisibility();
                    Dictionary<string,XrmPerformanceMarker>  perfResults = perf.GetMarkers();

                    //Iterate through markers
                }
            }
        }

        [TestMethod]
        public void TestActiveContactsView()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                var perf = xrmBrowser.PerformanceCenter;

                if (!perf.IsEnabled)
                    perf.IsEnabled = true;

                xrmBrowser.ThinkTime(500);

                xrmBrowser.Navigation.OpenSubArea("Sales", "Contacts");
                xrmBrowser.ThinkTime(4000);

                //var views = xrmBrowser.OpenViewPicker();
                xrmBrowser.Grid.SwitchView("Active Contacts");
                xrmBrowser.ThinkTime(1000);

                var items = xrmBrowser.Grid.GetGridItems().Value;

                foreach (var item in items)
                {
                    xrmBrowser.Entity.OpenEntity(item.Url);

                    perf.ToggleVisibility();
                    xrmBrowser.ThinkTime(1500);
                    perf.ToggleVisibility();
                    Dictionary<string, XrmPerformanceMarker> perfResults = perf.GetMarkers();


                    //Iterate through markers
                }
            }
        }

    }
   
}