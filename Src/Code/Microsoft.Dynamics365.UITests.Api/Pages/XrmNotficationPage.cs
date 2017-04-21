using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmNotficationPage
        : XrmPage
    {
        public XrmNotficationPage(InteractiveBrowser browser)
            : base(browser)
        {
        }
        
        public BrowserCommandResult<bool> CloseNotifications(int thinkTime = Constants.DefaultThinkTime)
        {
            Browser.ThinkTime(thinkTime);

            return this.Execute("Close Notifications", driver =>
            {
                Thread.Sleep(2000);
                while(driver.WaitUntilAvailable(By.Id("crmAppMessageBar"), new TimeSpan(0, 0, 10)) != null)
                {
                    driver.ClickWhenAvailable(By.Id("crmAppMessageBarCloseButton"), new TimeSpan(0, 0, 1));
                }

                driver.WaitForPageToLoad();
                return true;
            });
        }
    }
}