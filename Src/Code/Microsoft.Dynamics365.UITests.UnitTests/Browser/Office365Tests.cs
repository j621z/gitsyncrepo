using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;
using System;
using System.Security;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class Office365Tests
    {
        private readonly SecureString _username = "brkelly@mscrmrd.onmicrosoft.com".ToSecureString();
        private readonly SecureString _password = "T!T@n1130".ToSecureString();

		[TestMethod]
        public void TestLoginToOffice365()
        {
            using (var officeBrowser = new Office365Browser(new BrowserOptions
            {
                BrowserType = BrowserType.Chrome,
                PrivateMode = true
            }))
            {
                officeBrowser.LoginPage.Login(_username, _password);
            }
        }

		[TestMethod]
		public void TestOpenOffice365WaffleMenu()
		{
			using (var officeBrowser = new Office365Browser(new BrowserOptions
			{
				BrowserType = BrowserType.Chrome,
				PrivateMode = true
			}))
			{
				officeBrowser.LoginPage.Login(_username, _password);

				var menuItems = officeBrowser.Navigation.OpenWaffleMenu().Value;

				// Menu items should have a "CRM" item, if not the subscription doesn't have Dynamics CRM licenses or our test failed.
				Assert.IsTrue(menuItems.ContainsKey("CRM"));
			}
		}

		[TestMethod]
		public void TestOpenGladosInstancePicker()
		{
			using (var officeBrowser = new Office365Browser(new BrowserOptions
			{
				BrowserType = BrowserType.Chrome,
				PrivateMode = true
			}))
			{
				officeBrowser.LoginPage.Login(_username, _password);

				var orgs = officeBrowser.XrmInstancePicker.GetInstances();

				// 9 orgs in this subscription and the form should load in under 5 seconds.
				Assert.AreEqual(9, orgs.Value.Count);
				Assert.IsTrue(orgs.Result.ExecutionTime < TimeSpan.FromMilliseconds(5000));
			}
		}

    }
}