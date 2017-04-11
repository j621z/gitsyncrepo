using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmPage : BrowserPage
    {

        private static string relatedId;

        public XrmPage(InteractiveBrowser browser) : base(browser)
        {
        }

        public BrowserCommandResult<bool> SetValue(string field, bool check)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, check);
            return this.Execute($"Set Value: {field}", driver =>
            {
                if (driver.HasElement(By.Id("int_" + field)))
                {
                    var input = driver.FindElement(By.Id("int_" + field));

                    if (bool.Parse(input.FindElement(By.TagName("input")).GetAttribute("checked")) && !check)
                        input.FindElement(By.TagName("input")).Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(string field, DateTime date)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, date);
            return this.Execute($"Set Value: {field}", driver =>
            {
                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    //Check to see if focus is on field already
                    if (input.FindElement(By.ClassName("ms-crm-Inline-Edit")) != null)
                        input.FindElement(By.ClassName("ms-crm-Inline-Edit")).Click();
                    else
                        input.FindElement(By.ClassName("ms-crm-Inline-Value")).Click();

                    input.FindElement(By.TagName("input")).SendKeys(date.ToShortDateString());
                    input.FindElement(By.ClassName("ms-crm-Inline-Edit")).Click();

                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(string field, string value)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, value);
            return this.Execute($"Set Value: {field}", driver =>
            {
                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    //Check to see if focus is on field already
                    if (input.FindElement(By.ClassName("ms-crm-Inline-Edit")) != null)
                        input.FindElement(By.ClassName("ms-crm-Inline-Edit")).Click();
                    else
                        input.FindElement(By.ClassName("ms-crm-Inline-Value")).Click();

                    if (input.FindElements(By.TagName("textarea")).Count > 0)
                        input.FindElement(By.TagName("textarea")).SendKeys(value);
                    else
                        input.FindElement(By.TagName("input")).SendKeys(value);

                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(Field field)
        {
            return this.Execute($"Set Value: {field.Name}", driver =>
            {
                if (driver.HasElement(By.Id(field.Id)))
                {
                    var input = driver.FindElement(By.Id(field.Id));
                    input.Click();

                    //Check to see if focus is on field already
                    if (input.FindElement(By.ClassName("ms-crm-Inline-Edit")) != null)
                        input.FindElement(By.ClassName("ms-crm-Inline-Edit")).Click();
                    else
                        input.FindElement(By.ClassName("ms-crm-Inline-Value")).Click();

                    if (input.FindElements(By.TagName("textarea")).Count > 0)
                        input.FindElement(By.TagName("textarea")).SendKeys(field.Value);
                    else
                        input.FindElement(By.TagName("input")).SendKeys(field.Value);

                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(OptionSet option)
        {
            return this.Execute($"Set Value: {option.Name}", driver =>
            {
                if (driver.HasElement(By.Id(option.Name)))
                {
                    var input = driver.FindElement(By.Id(option.Name));
                    input.Click();

                    var select = input.FindElement(By.TagName("select"));
                    var options = select.FindElements(By.TagName("option"));

                    foreach (var op in options)
                    {
                        if (op.Text == option.Value || op.GetAttribute("value") == option.Value)
                            op.Click();
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {option.Name} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(CompositeControl control)
        {
            return this.Execute($"Set Conposite Control Value: {control.Id}", driver =>
            {
                if (!driver.HasElement(By.Id(control.Id)))
                    return false;

                driver.FindElement(By.Id(control.Id)).Click();

                if (driver.HasElement(By.Id(control.Id + "_compositionLinkControl_flyoutLoadingArea_flyOut")))
                {
                    var compcntrl =
                        driver.FindElement(By.Id(control.Id + "_compositionLinkControl_flyoutLoadingArea_flyOut"));

                    foreach (var field in control.Fields)
                    {
                        compcntrl.FindElement(By.Id("fullname_compositionLinkControl_" + field.Id)).Click();

                        var result = compcntrl.FindElements(By.TagName("input"))
                            .ToList()
                            .FirstOrDefault(i => i.GetAttribute("id").Contains(field.Id));

                        result?.SendKeys(field.Value);
                    }

                    compcntrl.FindElement(By.Id(control.Id + "_compositionLinkControl_flyoutLoadingArea-confirm")).Click();
                }
                else
                    throw new InvalidOperationException($"Composite Control: {control.Id} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Switches to content frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToContentFrame()
        {
            return this.Execute("Switch to content frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Frames.ContentPanel]));

                //find the crmContentPanel and find out what the current content frame ID is - then navigate to the current content frame
                var currentContentFrame = driver.FindElement(By.XPath(Elements.Xpath[Reference.Frames.ContentPanel]))
                                                .GetAttribute(Elements.ElementId[Reference.Frames.ContentFrameId]);

                driver.SwitchTo().Frame(currentContentFrame);

                return true;
            });
        }

        /// <summary>
        /// Switches to dialog frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToDialogFrame()
        {
            return this.Execute("Switch to dialog frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Frames.DialogFrame]));

                driver.SwitchTo().Frame(Elements.ElementId[Reference.Frames.DialogFrameId]);

                return true;
            });
        }

        /// <summary>
        /// Switches to Quick Find frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToQuickFindFrame()
        {
            return this.Execute("Switch to QuickFind Frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Frames.QuickFindFrame]));

                driver.SwitchTo().Frame(Elements.ElementId[Reference.Frames.QuickFindFrameId]);

                return true;
            });
        }

        /// <summary>
        /// Switches to related frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToRelatedFrame()
        {
            return this.Execute("Switch to Related Frame", driver =>
            {
                SwitchToContentFrame();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id(relatedId));

                driver.SwitchTo().Frame(relatedId + "Frame");

                return true;
            });
        }

    }
}
