﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using System.Security;
using Microsoft.Dynamics365.UITests.Browser;
using System.Threading;

namespace Microsoft.Dynamics365.UITests.UnitTests.Sample.EndUserScenerios
{
    [TestClass]
    public class CreateCase
    {
        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void TestCreateNewCase()
        {

            using (var xrmBrowser = new XrmBrowser(TestSettings.Options))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password);

                xrmBrowser.GuidedHelp.CloseGuidedHelp();

                xrmBrowser.ThinkTime(500);
                xrmBrowser.Navigation.OpenSubArea("Sales", "Accounts");

                xrmBrowser.ThinkTime(3000);
                xrmBrowser.Grid.OpenRecord(0);
                xrmBrowser.Navigation.OpenRelated("Cases");

                xrmBrowser.Related.SwitchView("Active Cases");
                xrmBrowser.ThinkTime(2000);
               
                xrmBrowser.CommandBar.ClickCommand("Add New Case");
                xrmBrowser.ThinkTime(2000);


                xrmBrowser.Entity.SetValue("title_cl", "Test API Case");
                
                xrmBrowser.CommandBar.ClickCommand("globalquickcreate_save_button_NavBarGloablQuickCreate");


            }
        }
    }
}
