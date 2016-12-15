using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;

namespace Microsoft.Xrm.UITests.Api
{
    public class XrmGuidedHelpPage
        : BrowserPage
    {
        public XrmGuidedHelpPage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        public bool IsEnabled
        {
            get
            {
                bool isGuidedHelpEnabled = false;
                bool.TryParse(
                    Driver.ExecuteScript("return Xrm.Internal.isFeatureEnabled('FCB.GuidedHelp') && Xrm.Internal.isGuidedHelpEnabledForUser();").ToString(),
                    out isGuidedHelpEnabled);

                return isGuidedHelpEnabled;
            }
        }

        public BrowserCommandResult<bool> CloseGuidedHelp()
        {
            return this.Execute("Close Guided Help", driver =>
            {
                bool returnValue = false;

                if (IsEnabled)
                {
                    driver.WaitUntilVisible(By.Id("marsOverlay"), new TimeSpan(0, 0, 10), d =>
                    {
                        var allMarsElements = driver
                            .FindElement(By.Id("marsOverlay"))
                            .FindElements(By.XPath(".//*"));

                        foreach (var element in allMarsElements)
                        {
                            var buttonId = driver.ExecuteScript("return arguments[0].id;", element).ToString();

                            if (buttonId.Equals("closeButton", StringComparison.InvariantCultureIgnoreCase))
                            {
                                driver.WaitUntilVisible(By.Id(buttonId), new TimeSpan(0, 0, 2));

                                element.Click();
                            }
                        }

                        returnValue = true;
                    });
                }

                return returnValue;
            });
        }

        [Obsolete("Modal dialogs are no longer considered supported for most modern browsers", false)]
        public BrowserCommandResult<bool> CloseModalDialogs()
        {
            return this.Execute("Close Modal Dialog", driver =>
            {
                // Get the current window handles
                string popupHandle = string.Empty;
                ReadOnlyCollection<string> windowHandles = driver.WindowHandles;
                string mainWindow = driver.CurrentWindowHandle;

                foreach (string handle in windowHandles)
                {
                    if (handle != driver.CurrentWindowHandle)
                    {
                        popupHandle = handle; break;
                    }
                }

                // Switch to any new windows launched - this should be a non issue now
                if (popupHandle.Length != 0)
                {
                    driver.SwitchTo().Window(popupHandle);

                    // Check for element on new page
                    // In this case is get the email of the alert message to click and close the window
                    var webElement = driver.FindElement(By.Id("butBegin"));

                    if (webElement.Text == "OK")
                    {
                        driver.FindElement(By.Id("butBegin")).Click();
                    }
                }

                // Switch back to original window
                driver.SwitchTo().Window(mainWindow);

                return true;
            });
        }

        public BrowserCommandResult<bool> CloseWelcomeTour()
        {
            return this.Execute("Close Welcome Tour", driver =>
            {
                // Close the email and nav tour approval dialog if it's there - go top to bottom (reverse)
                foreach (var frame in driver.FindElements(By.TagName("iframe")).Reverse())
                {
                    // navigate down into the frame
                    driver.SwitchTo().Frame(frame);

                    // look for nav tour
                    if (driver.HasElement(By.Id("butBegin")))
                    {
                        driver.FindElement(By.Id("butBegin")).Click();

                        Thread.Sleep(1000);

                        driver.SwitchTo().ParentFrame();

                        continue;
                    }

                    if (driver.HasElement(By.Id("buttonClose")))
                    {
                        driver.FindElement(By.Id("buttonClose")).Click();

                        Thread.Sleep(1000);

                        driver.SwitchTo().ParentFrame();

                        continue;
                    }

                    driver.SwitchTo().ParentFrame();
                }

                return true;
            });
        }
    }
}