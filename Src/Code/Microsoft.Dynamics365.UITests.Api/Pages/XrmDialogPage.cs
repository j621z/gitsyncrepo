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
        /// <summary>
        /// Enum for the Assign Dialog to determine which type of record you will be assigning to. 
        /// </summary>
        public enum AssignTo
        {
            Me,
            User,
            Team
        }
        /// <summary>
        /// Closes the opportunity.
        /// </summary>
        /// <param name="revenue">The revenue.</param>
        /// <param name="closeDate">The close date.</param>
        /// <param name="description">The description.</param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> CloseOpportunity(double revenue, DateTime closeDate, string description, int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);
            return this.Execute("Close Opportunity", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.Header]),
                                          new TimeSpan(0, 0, 10), 
                                          "The Close Opportunity dialog is not available.");

                SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.ActualRevenueId], revenue.ToString());
                SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.CloseDateId], closeDate);
                SetValue(Elements.ElementId[Reference.Dialogs.CloseOpportunity.DescriptionId], description);

                driver.FindElement(By.XPath(Elements.Xpath[Reference.Dialogs.CloseOpportunity.Ok]))
                      .Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> Assign(AssignTo to, string value, int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Assign", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.Header]),
                                          new TimeSpan(0, 0, 10),
                                          "The Assign dialog is not available.");

                switch (to)
                {
                    case AssignTo.Me:
                        driver.FindElement(By.XPath(Elements.Xpath[Reference.Dialogs.Assign.Ok]))
                          .Click();
                        break;

                    case AssignTo.User:
                        this.SetLookup(Elements.ElementId[Reference.Dialogs.Assign.UserOrTeamLookupId], value);
                        break;

                    case AssignTo.Team:
                        this.SetLookup(Elements.ElementId[Reference.Dialogs.Assign.UserOrTeamLookupId], true);
                        break;
                }

                return true;
            });
        }
    }
}
