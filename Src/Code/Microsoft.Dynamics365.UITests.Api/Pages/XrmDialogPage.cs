﻿using OpenQA.Selenium;
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
        /// Closes the opportunity you are currently working on.
        /// </summary>
        /// <param name="revenue">The revenue you want to assign to the opportunity.</param>
        /// <param name="closeDate">The close date for the opportunity.</param>
        /// <param name="description">The description of the closing.</param>
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

        /// <summary>
        /// Assigns the record to a User or Team
        /// </summary>
        /// <param name="to">The User or Team you want to assign the record to</param>
        /// <param name="value">The value of the User or Team you want to find and select</param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
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

        /// <summary>
        /// Deletes the selected record.
        /// </summary>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Delete(int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Delete", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.Header]),
                                          new TimeSpan(0, 0, 10),
                                          "The Delete dialog is not available.");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.Dialogs.Delete.Ok]))
                      .Click();

                return true;
            });
        }

        /// <summary>
        /// Selects the Business Process Flow from the Dialog.
        /// </summary>
        /// <param name="name">The name of the business process flow you want to select.</param>
        /// <param name="thinkTime"></param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectBusinessProcessFlow(string name, int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Delete", driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.Header]),
                                          new TimeSpan(0, 0, 10),
                                          "The Select Business Process Flow dialog is not available.");

                var processes = driver.FindElements(By.ClassName(Elements.CssClass[Reference.Dialogs.SwitchProcess.Ok]));
                IWebElement element = null; 

                foreach(var process in processes)
                {
                    if (process.GetAttribute("title") == name)
                        element = process;
                }

                if (element != null)
                    element.Click();
                else
                    throw new InvalidOperationException($"The Business Process with name: '{name}' does not exist");

                return true;
            });
        }
    }
}
