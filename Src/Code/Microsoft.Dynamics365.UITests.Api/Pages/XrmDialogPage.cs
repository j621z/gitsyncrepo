using OpenQA.Selenium;
using System;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmDialogPage
       : XrmPage
    {
        public XrmDialogPage(InteractiveBrowser browser)
            : base(browser)
        {
            this.SwitchToDialogFrame();
        }

        public BrowserCommandResult<bool> CloseOpportunity(double revenue, DateTime closeDate, string description, int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);
            return this.Execute("Close Opportunity", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.Header]),
                                          new TimeSpan(0, 0, 10), 
                                          "The Close Opportunity dialog is not available.");

                this.SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.ActualRevenueId], revenue.ToString());
                this.SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.CloseDateId], closeDate);
                this.SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.DescriptionId], description);

                driver.FindElement(By.XPath(Elements.Xpath[Reference.Dialogs.CloseOpportunity.Ok]))
                      .Click();

                return true;
            });
        }
    }
}
