﻿// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.NegativeScenarios.BusinessProcessFlow
{
    [TestClass]
    public class InvalidPreviousStage
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestInvalidPreviousStage()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.ThinkTime(2000);
                xrmBrowser.Grid.SwitchView("Open Opportunities");

                xrmBrowser.ThinkTime(1000);
                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.PreviousStage();

                xrmBrowser.ThinkTime(1000);
            }
        }

    }
}


