using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Security;
using System.Threading;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class AACreateLead
    {

        private readonly SecureString _username = System.Configuration.ConfigurationManager.AppSettings["OnlineUsername"].ToSecureString();
        private readonly SecureString _password = System.Configuration.ConfigurationManager.AppSettings["OnlinePassword"].ToSecureString();
        private readonly Uri _xrmUri = new Uri(System.Configuration.ConfigurationManager.AppSettings["OnlineCrmUrl"].ToString());

        [TestMethod]
        public void CreateNewLead()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password, ADFSLogin);

                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                xrmBrowser.Notifications.CloseNotifications();

                xrmBrowser.Navigation.OpenHomePage();
                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");
                
                xrmBrowser.CommandBar.ClickCommand("New");                

                xrmBrowser.Entity.SetValue("subject", "Test API to Create leads");
                xrmBrowser.Entity.SetValue("firstname", "Test");
                xrmBrowser.Entity.SetValue("lastname", "API Lead");
                xrmBrowser.Entity.SetValue("telephone1", "555-555-1234");
                xrmBrowser.Entity.SetValue("emailaddress1", "test@contoso.com");
                xrmBrowser.Entity.SetValue("address2_line1", "100 Main St");
                xrmBrowser.Entity.SetValue("address2_city", "Seattle");
                xrmBrowser.Entity.SetValue("address2_postalcode", "55555");
                xrmBrowser.Entity.SelectLookup("aa_countryid", 0);
                xrmBrowser.Entity.SetValue("companyname", "MyCompany123");
                xrmBrowser.Entity.SetValue(new OptionSet() { Name = "aa_leadtype", Value = "100000003" });
                xrmBrowser.Entity.SetValue(new OptionSet() { Name = "aa_product_interest", Value = "100000000" });
                xrmBrowser.Entity.SetValue(new OptionSet() { Name = "aa_leadsourceglobal", Value = "100000001" });
                
                
                xrmBrowser.CommandBar.ClickCommand("Save");

                xrmBrowser.Navigation.OpenRelated("Activities");

                xrmBrowser.Related.SwitchView("All Activities");
                
                //Add in delay to review test results
                xrmBrowser.ThinkTime(5000);
            }
        }

        [TestMethod]
        public void QualifyLead()
        {
            using (var xrmBrowser = new XrmBrowser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true,
                FireEvents = true
            }))
            {
                xrmBrowser.LoginPage.Login(_xrmUri, _username, _password, ADFSLogin);
                xrmBrowser.GuidedHelp.CloseGuidedHelp();
                xrmBrowser.Notifications.CloseNotifications();

                xrmBrowser.Navigation.OpenSubArea("Sales", "Leads");

                //************
                //This is broken for AA because of duplicate view names. 
                //************
                xrmBrowser.Grid.SwitchView("All Leads");

                xrmBrowser.Grid.Search("Test API");

                xrmBrowser.ThinkTime(1000);

                xrmBrowser.Grid.OpenGridRecord(0);

                xrmBrowser.Entity.SetValue("aa_scrubbed", true);
                xrmBrowser.Entity.SetValue("jobtitle", "Manager");
                
                xrmBrowser.CommandBar.ClickCommand("Qualify", "Qualify (Account, Contact, Opportunity)");

                xrmBrowser.ThinkTime(2000);
            }
        }

        public void ADFSLogin(LoginRedirectEventArgs args)
        {
            var d = args.Driver;
               
            d.FindElement(By.Id("passwordInput")).SendKeys(args.Password.ToUnsecureString());

            d.ClickWhenAvailable(By.Id("submitButton"), new TimeSpan(0, 0, 2));

            d.WaitForPageToLoad();
        }
    }

}