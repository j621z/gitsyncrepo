using OpenQA.Selenium;
using System;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;
using System.Collections.Generic;

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
                while(driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Notification.AppMessageBar]), new TimeSpan(0, 0, 10)) != null)
                {
                    driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.Notification.Close]), new TimeSpan(0, 0, 1));
                }

                driver.WaitForPageToLoad();
                return true;
            });
        }
        public BrowserCommandResult<bool> Dismiss(XrmAppNotification notification)
        {
            return Dismiss(notification.Index);
        }

        public BrowserCommandResult<bool> Dismiss(Int32 index)
        {
            return this.Execute("Dismiss App Message", driver =>
            {
                bool returnValue = false;

                driver.WaitUntilVisible(By.Id("crmAppMessageBar"), new TimeSpan(0, 0, 5), d =>
                {
                    var container = driver.FindElement(By.Id("crmAppMessageBar"));
                    var rows = container.FindElements(By.ClassName("crmAppMessageBarRow"));

                    if (rows.Count > index)
                    {
                        var row = rows[index];
                        var dismissButtonElement = row.FindElement(By.ClassName("crmAppMessageBarButtonContainer"));
                        var dismissButton = dismissButtonElement.FindElement(By.TagName("a"));

                        dismissButton.Click();

                        returnValue = true;
                    }
                });

                return returnValue;
            });
        }

        public BrowserCommandResult<List<XrmAppNotification>> Notifications
        {
            get
            {
                return this.Execute("Get App Messages", driver =>
                {
                    var returnList = new List<XrmAppNotification>();
                    var index = 0;

                    driver.WaitUntilVisible(By.Id("crmAppMessageBar"), new TimeSpan(0, 0, 5), d =>
                    {
                        var container = driver.FindElement(By.Id("crmAppMessageBar"));
                        var rows = container.FindElements(By.ClassName("crmAppMessageBarRow"));

                        foreach (var row in rows)
                        {
                            var titleElement = row.FindElement(By.ClassName("crmAppMessageBarTitle"));
                            var messageElement = row.FindElement(By.ClassName("crmAppMessageBarMessage"));
                            var dismissButtonElement = row.FindElement(By.ClassName("crmAppMessageBarButtonContainer"));

                            var newItem = new XrmAppNotification(this)
                            {
                                Index = index,
                                Title = titleElement.Text,
                                Message = messageElement.Text,
                                DismissButtonText = dismissButtonElement.Text
                            };

                            returnList.Add(newItem);

                            index++;
                        }
                    });

                    return returnList;
                });
            }
        }
    }
}