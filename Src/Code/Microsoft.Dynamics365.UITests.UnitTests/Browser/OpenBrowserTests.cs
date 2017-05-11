using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.Dynamics365.UITests.Api;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.UnitTests
{
    [TestClass]
    public class OpenWebBrowserTests
    {
	    private readonly string _homePageUri = "http://www.bing.com";

        [TestMethod]
        public void TestOpenInternetExplorer()
        {
            using (var browser = new InteractiveBrowser(TestSettings.Options))
            {
                browser.Driver.Navigate().GoToUrl(_homePageUri);
            }
        }

		[TestMethod]
		public void TestOpenChrome()
		{
			using (var browser = new InteractiveBrowser(TestSettings.Options))
			{
				browser.Driver.Navigate().GoToUrl(_homePageUri);
			}
		}

		[TestMethod]
		public void TestOpenFirefox()
		{
			using (var browser = new InteractiveBrowser(TestSettings.Options))
			{
				browser.Driver.Navigate().GoToUrl(_homePageUri);
			}
		}

		[TestMethod]
		public void TestOpenEdge()
		{
			using (var browser = new InteractiveBrowser(TestSettings.Options))
			{
				browser.Driver.Navigate().GoToUrl(_homePageUri);
			}
		}

		[TestMethod]
		public void TestOpenPhantomJs()
		{
			using (var browser = new InteractiveBrowser(TestSettings.Options))
			{
				browser.Driver.Navigate().GoToUrl(_homePageUri);
			}
		}
	}
}