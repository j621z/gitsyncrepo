﻿using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmBusinessProcessFlow
        : XrmPage
    {
        public XrmBusinessProcessFlow(InteractiveBrowser browser)
            : base(browser)
        {
            SwitchToContentFrame();
        }

        /// <summary>
        /// Moves to the Next stage in the Business Process Flow.
        /// </summary>
        /// <param name="ThinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> NextStage(int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Next Stage", driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.NextStage])))
                    throw new Exception("Business Process Flow Next Stage Element does not exist");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.NextStage]))
                      .Click();

                return true;
            });
        }

        /// <summary>
        /// Moves to the Previous stage in the Business Process Flow.
        /// </summary>
        /// <param name="ThinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> PreviousStage(int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Previous Stage", driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.PreviousStage])))
                    throw new Exception("Business Process Flow Previous Stage Element does not exist");
                
                driver.FindElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.PreviousStage]))
                      .Click();

                return true;
            });
        }

        /// <summary>
        /// Hides the Business Process flow UI.
        /// </summary>
        /// <param name="thinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> Hide(int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Hide", driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.Hide])))
                    throw new Exception("Business Process Flow Next Stage Element does not exist");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.Hide]))
                      .Click();

                return true;
            });
        }
        /// <summary>
        /// Selects the Business Process Flow stage.
        /// </summary>
        /// <param name="stagenumber">The stage number that you would like to select. The stages start with 0.</param>
        /// <param name="thinkTime">Used to simulate a wait time between human interactions. The Default is 2 seconds.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SelectStage(int stagenumber, int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Select Stage", driver =>
            {
                var xpath = Elements.Xpath[Reference.BusinessProcessFlow.SelectStage].Replace("[STAGENUM]", stagenumber.ToString());

                if (!driver.HasElement(By.XPath(xpath)))
                    throw new Exception("Business Process Flow Select Stage Element does not exist");

                driver.FindElement(By.XPath(xpath))
                      .Click();

                return true;
            });
        }

        /// <summary>
        /// Sets the current selected Stage as Active.
        /// </summary>
        /// <param name="thinkTime">The think time.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetActive(int thinkTime = Constants.DefaultThinkTime)
        {
            this.Browser.ThinkTime(thinkTime);

            return this.Execute("Set Active", driver =>
            {
                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.SetActive])))
                    throw new Exception("Business Process Flow Set Active Element does not exist");

                driver.FindElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.SetActive]))
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

            return this.Execute(GetOptions("Delete"), driver =>
            {
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Dialogs.ProcessFlowHeader]),
                                          new TimeSpan(0, 0, 10),
                                          "The Select Business Process Flow dialog is not available.");

                var processes = driver.FindElements(By.ClassName(Elements.CssClass[Reference.Dialogs.SwitchProcess.Process]));
                IWebElement element = null;

                foreach (var process in processes)
                {
                    if (process.Text == name)
                        element = process;
                }

                if (element != null)
                    element.Click();
                else
                    throw new InvalidOperationException($"The Business Process with name: '{name}' does not exist");

                driver.ClickWhenAvailable(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.Ok]));

                return true;
            });
        }
    }
}