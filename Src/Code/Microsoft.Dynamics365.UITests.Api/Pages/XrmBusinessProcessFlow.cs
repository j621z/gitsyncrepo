using OpenQA.Selenium;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using Microsoft.Dynamics365.UITests.Browser;

/// <summary>
/// 
/// </summary>
namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmBusinessProcessFlow
        : BrowserPage
    {
        public XrmBusinessProcessFlow(InteractiveBrowser browser)
            : base(browser)
        {
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
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (!driver.HasElement(By.XPath(Elements.Xpath[Reference.BusinessProcessFlow.PreviousStage])))
                    throw new Exception("Business Process Flow Next Stage Element does not exist");
                
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
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
                this.Browser.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var xpath = Elements.Xpath[Reference.BusinessProcessFlow.SelectStage].Replace("[STAGENUM]", stagenumber.ToString());

                if (!driver.HasElement(By.XPath(xpath)))
                    throw new Exception("Business Process Flow Next Stage Element does not exist");

                driver.FindElement(By.XPath(xpath))
                      .Click();

                return true;
            });
        }
    }
}