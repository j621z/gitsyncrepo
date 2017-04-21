using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmAppMessageBarPage
        : XrmPage
    {
        public XrmAppMessageBarPage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        public BrowserCommandResult<bool> Dismiss(XrmAppMessage message)
        {
            return Dismiss(message.Index);
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

        public BrowserCommandResult<List<XrmAppMessage>> Messages
        {
            get
            {
                return this.Execute("Get App Messages", driver =>
                {
                    var returnList = new List<XrmAppMessage>();
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

                            var newItem = new XrmAppMessage(this)
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