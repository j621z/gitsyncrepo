// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT license.

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UIAutomation.Api;
using Microsoft.Dynamics365.UIAutomation.Browser;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample.BusinessProcessFlow
{
    [TestClass]
    public class BusinessProcessFlow
    {
       
        private readonly SecureString  _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestBusinessProcessFlow()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                
                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

                xrmBrowser.BusinessProcessFlow.PreviousStage();

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowNextStage()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowPreviousStage()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.NextStage();

                xrmBrowser.BusinessProcessFlow.PreviousStage();

            }
        }
        [TestMethod]
        public void TestBusinessProcessFlowHide()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

            }
        }

        [TestMethod]
        public void TestBusinessProcessFlowSelectStage()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);
            }
        }


        [TestMethod]
        public void TestBusinessProcessFlowSetActive()
        {
            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Opportunities");

                xrmBrowser.Grid.SwitchView("My Open Opportunities");

                xrmBrowser.Grid.OpenRecord(0);

                xrmBrowser.BusinessProcessFlow.Hide();

                xrmBrowser.BusinessProcessFlow.SelectStage(0);

                xrmBrowser.BusinessProcessFlow.SetActive();
            }
        }

    }
}