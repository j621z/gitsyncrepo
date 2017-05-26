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

        /// <summary>
        /// Sets the value of a Checkbox field.
        /// </summary>
        /// <param name="field">Field name or ID.</param>
        /// <param name="check">If set to <c>true</c> [check].</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(string field, bool check)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, check);
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
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

        /// <summary>
        /// Sets the value of a Date Field.
        /// </summary>
        /// <param name="field">The field id or name.</param>
        /// <param name="date">DateTime value.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(string field, DateTime date)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, date);
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                if (driver.HasElement(By.Id(field)))
                {
                    var fieldElement = driver.FindElement(By.Id(field));
                    fieldElement.Click();

                    //Check to see if focus is on field already
                    if (fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])) != null)
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])).Click();
                    else
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.ValueClass])).Click();

                    var input = fieldElement.FindElement(By.TagName("input"));

                    if (input.GetAttribute("value").Length > 0)
                    {
                        input.Clear();
                        fieldElement.Click();
                        input.SendKeys(date.ToShortDateString());
                        input.SendKeys(Keys.Enter);
                    }
                    else
                    {
                        input.SendKeys(date.ToShortDateString());
                        input.SendKeys(Keys.Enter);
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a Text/Description field.
        /// </summary>
        /// <param name="field">The field id.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(string field, string value)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, value);
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                if (driver.HasElement(By.Id(field)))
                {
                    driver.WaitUntilVisible(By.Id(field));

                    var fieldElement = driver.FindElement(By.Id(field));
                    fieldElement.Click();

                    //Check to see if focus is on field already
                    if (fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])) != null)
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])).Click();
                    else
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.ValueClass])).Click();

                    if (fieldElement.FindElements(By.TagName("textarea")).Count > 0)
                    {
                        fieldElement.FindElement(By.TagName("textarea")).Clear();
                        fieldElement.FindElement(By.TagName("textarea")).SendKeys(value);
                    }
                    else
                    {
                        fieldElement.FindElement(By.TagName("input")).Clear();
                        fieldElement.FindElement(By.TagName("input")).SendKeys(value);
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a Field.
        /// </summary>
        /// <param name="field">The field .</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(Field field)
        {
            return this.Execute(GetOptions($"Set Value: {field.Name}"), driver =>
            {
                driver.WaitUntilVisible(By.Id(field.Id));

                if (driver.HasElement(By.Id(field.Id)))
                {
                    var fieldElement = driver.FindElement(By.Id(field.Id));
                    fieldElement.Click();

                    //Check to see if focus is on field already
                    if (fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])) != null)
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])).Click();
                    else
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.ValueClass])).Click();


                    if (fieldElement.FindElements(By.TagName("textarea")).Count > 0)
                    {
                        fieldElement.FindElement(By.TagName("textarea")).Clear();
                        fieldElement.FindElement(By.TagName("textarea")).SendKeys(field.Value);
                    }
                    else
                    {
                        fieldElement.FindElement(By.TagName("input")).Clear();
                        fieldElement.FindElement(By.TagName("input")).SendKeys(field.Value);
                    }

                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a picklist.
        /// </summary>
        /// <param name="option">The option you want to set.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(OptionSet option)
        {
            return this.Execute(GetOptions($"Set Value: {option.Name}"), driver =>
            {
                driver.WaitUntilVisible(By.Id(option.Name));

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

        /// <summary>
        /// Sets the value of a Composite control.
        /// </summary>
        /// <param name="control">The Composite control values you want to set.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(CompositeControl control)
        {
            return this.Execute(GetOptions($"Set Conposite Control Value: {control.Id}"), driver =>
            {
                driver.WaitUntilVisible(By.Id(control.Id));

                if (!driver.HasElement(By.Id(control.Id)))
                    return false;

                driver.FindElement(By.Id(control.Id)).Click();

                if (driver.HasElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.FlyOut])))
                {
                    var compcntrl =
                        driver.FindElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.FlyOut]));

                    foreach (var field in control.Fields)
                    {
                        compcntrl.FindElement(By.Id(Elements.ElementId[Reference.SetValue.CompositionLinkControl] + field.Id)).Click();

                        var result = compcntrl.FindElements(By.TagName("input"))
                            .ToList()
                            .FirstOrDefault(i => i.GetAttribute("id").Contains(field.Id));

                        result?.SendKeys(field.Value);
                    }

                    compcntrl.FindElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.Confirm])).Click();
                }
                else
                    throw new InvalidOperationException($"Composite Control: {control.Id} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Sets the value of a Lookup.
        /// </summary>
        /// <param name="control">The lookup field name, value or index of the lookup.</param>
        /// <returns></returns>
        public BrowserCommandResult<bool> SetValue(Lookup control)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {control.Name}"), driver =>
            {
                if (driver.HasElement(By.Id(control.Name)))
                {
                    driver.WaitUntilVisible(By.Id(control.Name));

                    var input = driver.FindElement(By.Id(control.Name));
                    input.Click();

                    if (input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])) == null)
                        throw new InvalidOperationException($"Field: {control.Name} is not lookup");

                    input.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.LookupRenderClass])).Click();

                    var dialogName = $"Dialog_{control.Name}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if(control.Value != null)
                    {
                        if (!dialogItems.Keys.Contains(control.Value))
                            throw new InvalidOperationException($"List does not have {control.Value}.");

                        var dialogItem = dialogItems[control.Value];
                        dialogItem.Click();
                    }
                    else
                    { 
                        if (dialogItems.Count < control.Index)
                            throw new InvalidOperationException($"List does not have {control.Index + 1} items.");

                        var dialogItem = dialogItems.Values.ToList()[control.Index];
                        dialogItem.Click();
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {control.Name} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Gets the value of a Text/Description field.
        /// </summary>
        /// <param name="field">The field id.</param>
        /// <returns>The value</returns>
        public BrowserCommandResult<string> GetValue(string field)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, value);
            return this.Execute($"Set Value: {field}", driver =>
            {
                driver.WaitUntilVisible(By.Id(field));

                string text = string.Empty;
                if (driver.HasElement(By.Id(field)))
                {
                    driver.WaitUntilVisible(By.Id(field));
                    var fieldElement = driver.FindElement(By.Id(field));
                    fieldElement.Click();

                    if (fieldElement.FindElements(By.TagName("textarea")).Count > 0)
                    {
                        text = fieldElement.FindElement(By.TagName("textarea")).GetAttribute("value") ;
                    }
                    else
                    {
                        text = fieldElement.FindElement(By.TagName("input")).GetAttribute("value");

                    }
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return text;
            });
        }

        /// <summary>
        /// Gets the value of a Field.
        /// </summary>
        /// <param name="field">The field .</param>
        /// <returns>The value</returns>
        public BrowserCommandResult<bool> GetValue(Field field)
        {
            return this.Execute($"Set Value: {field.Name}", driver =>
            {
                var text = string.Empty;

                driver.WaitUntilVisible(By.Id(field.Id));

                if (driver.HasElement(By.Id(field.Id)))
                {
                    var fieldElement = driver.FindElement(By.Id(field.Id));
                    fieldElement.Click();

                    //Check to see if focus is on field already
                    if (fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])) != null)
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.EditClass])).Click();
                    else
                        fieldElement.FindElement(By.ClassName(Elements.CssClass[Reference.SetValue.ValueClass])).Click();


                    if (fieldElement.FindElements(By.TagName("textarea")).Count > 0)
                    {
                        fieldElement.FindElement(By.TagName("textarea")).GetAttribute("value");
                    }
                    else
                    {
                        fieldElement.FindElement(By.TagName("input")).GetAttribute("value");
                    }

                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        /// <summary>
        /// Gets the value of a Composite control.
        /// </summary>
        /// <param name="control">The Composite control values you want to set.</param>
        /// <returns></returns>
        public BrowserCommandResult<string> GetValue(CompositeControl control)
        {
            return this.Execute($"Set Conposite Control Value: {control.Id}", driver =>
            {
                string text = string.Empty;

                driver.WaitUntilVisible(By.Id(control.Id));

                driver.FindElement(By.Id(control.Id)).Click();

                if (driver.HasElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.FlyOut])))
                {
                    var compcntrl =
                        driver.FindElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.FlyOut]));

                    foreach (var field in control.Fields)
                    {
                        compcntrl.FindElement(By.Id(Elements.ElementId[Reference.SetValue.CompositionLinkControl] + field.Id)).Click();

                        var result = compcntrl.FindElements(By.TagName("input"))
                            .ToList()
                            .FirstOrDefault(i => i.GetAttribute("id").Contains(field.Id));
                        text += result.GetAttribute("value");
                    }

                    compcntrl.FindElement(By.Id(control.Id + Elements.ElementId[Reference.SetValue.Confirm])).Click();
                }
                else
                    throw new InvalidOperationException($"Composite Control: {control.Id} Does not exist");

                return text;
            });
        }

        /// <summary>
        /// Gets the value of a picklist.
        /// </summary>
        /// <param name="option">The option you want to set.</param>
        /// <returns></returns>
        public BrowserCommandResult<string> GetValue(OptionSet option)
        {
            return this.Execute($"Set Value: {option.Name}", driver =>
            {
                driver.WaitUntilVisible(By.Id(option.Name));
                string text = string.Empty;
                if (driver.HasElement(By.Id(option.Name)))
                {
                    var input = driver.FindElement(By.Id(option.Name));
                    text = input.Text;                
                }
                else
                    throw new InvalidOperationException($"Field: {option.Name} Does not exist");

                return text;
            });
        }

        /// <summary>
        /// Gets the value of a Lookup.
        /// </summary>
        /// <param name="control">The lookup field name, value or index of the lookup.</param>
        /// <returns></returns>
        public BrowserCommandResult<string> GetValue(Lookup control)
        {
            return this.Execute($"Get Lookup Value: {control.Name}", driver =>
            {
                driver.WaitUntilVisible(By.Id(control.Name));

                string lookupValue = string.Empty;
                if (driver.HasElement(By.Id(control.Name)))
                {
                    var input = driver.FindElement(By.Id(control.Name));
                    lookupValue = input.Text;                  
                }
                else
                    throw new InvalidOperationException($"Field: {control.Name} Does not exist");

                return lookupValue;
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
        public bool SwitchToDialogFrame(int frameIndex = 0)
        {
            return this.Execute("Switch to dialog frame", driver =>
            {
                var index = "";
                if (frameIndex > 0)
                    index = frameIndex.ToString();

                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Frames.DialogFrame]+index));

                driver.SwitchTo().Frame(Elements.ElementId[Reference.Frames.DialogFrameId].Replace("[INDEX]",index));

                return true;
            });
        }

        /// <summary>
        /// Switches to Quick Find frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToQuickCreateFrame()
        {
            return this.Execute("Switch to Quick Create Frame", driver =>
            {
                driver.SwitchTo().DefaultContent();
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.XPath(Elements.Xpath[Reference.Frames.QuickCreateFrame]));

                driver.SwitchTo().Frame(Elements.ElementId[Reference.Frames.QuickCreateFrameId]);

                return true;
            });
        }

        /// <summary>
        /// Switches to related frame in the CRM application.
        /// </summary>
        /// <returns></returns>
        public bool SwitchToRelatedFrame()
        {

            SwitchToContentFrame();

            return this.Execute("Switch to Related Frame", driver =>
            {
                //wait for the content panel to render
                driver.WaitUntilAvailable(By.Id(Browser.ActiveFrameId));

                driver.SwitchTo().Frame(Browser.ActiveFrameId + "Frame");

                return true;
            });
        }

        public bool SwitchToDefaultContent()
        {
            return this.Execute("Switch to Default Content", driver =>
            {
                driver.SwitchTo().DefaultContent();

                return true;
            });
        }

        internal BrowserCommandOptions GetOptions(string commandName)
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                commandName,
                0,
                0,
                null,
                true,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }

        private BrowserCommandResult<Dictionary<string, IWebElement>> OpenDialog(IWebElement dialog)
        {
            var dictionary = new Dictionary<string, IWebElement>();
            var dialogItems = dialog.FindElements(By.TagName("li"));

            foreach (var dialogItem in dialogItems)
            {
                if (dialogItem.GetAttribute("role") != null && dialogItem.GetAttribute("role") == "menuitem")
                {
                    var links = dialogItem.FindElements(By.TagName("a"));

                    if (links != null && links.Count > 1)
                    {
                        var title = links[1].GetAttribute("title");

                        dictionary.Add(title, links[1]);
                    }
                }
            }
            return dictionary;
        }

       
    }
}
