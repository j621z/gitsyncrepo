using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.PhantomJS;
using OpenQA.Selenium.Support.Events;
using System;

namespace Microsoft.Xrm.UITests.Api
{
    public static class BrowserDriverFactory
    {
        public static IWebDriver CreateWebDriver(BrowserOptions options)
        {
            IWebDriver driver;
           
            switch (options.BrowserType)
            {
                case BrowserType.Chrome:
                    driver = new ChromeDriver(options.DriversPath, options.ToChrome());

                    break;
                case BrowserType.IE:
                    driver = new InternetExplorerDriver(options.DriversPath, options.ToInternetExplorer(), TimeSpan.FromMinutes(20));

                    break;
                case BrowserType.Firefox:
                    driver = new FirefoxDriver();

                    break;
                case BrowserType.PhantomJs:
                    driver = new PhantomJSDriver(options.DriversPath);

                    break;
                default:
                    throw new InvalidOperationException(
                        $"The browser type '{options.BrowserType}' is not recognized.");
            }

            driver.Manage().Timeouts().SetPageLoadTimeout(options.PageLoadTimeout);

            if (options.FireEvents || options.EnableRecording)
            {
                // Wrap the newly created driver.
                driver = new EventFiringWebDriver(driver);
            }

            return driver;
        }
    }
}