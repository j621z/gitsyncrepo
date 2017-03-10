using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Microsoft.Dynamics365.UITests.Browser
{
    public class XrmQuickCreatePage
        : BrowserPage
    {
        public XrmQuickCreatePage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        internal BrowserCommandOptions GetOptions(string commandName)
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                commandName,
                1,
                0,
                null,
                false,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }
        
        public BrowserCommandResult<bool> Cancel()
        {
            return this.Execute(GetOptions("Cancel"), driver =>
            {
                driver.SwitchTo().DefaultContent();

                driver.FindElement(By.Id("globalquickcreate_cancel_button_NavBarGloablQuickCreate"))?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> Save()
        {
            return this.Execute(GetOptions("Save"), driver =>
            {
                driver.SwitchTo().DefaultContent();

                driver.FindElement(By.Id("globalquickcreate_save_button_NavBarGloablQuickCreate"))?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (dialogItems.Count < index)
                        throw new InvalidOperationException($"List does not have {index + 1} items.");

                    var dialogItem = dialogItems.Values.ToList()[index];
                    dialogItem.Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, string value)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    var lookupIcon = input.FindElement(By.ClassName("Lookup_RenderButton_td"));
                    lookupIcon.Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (!dialogItems.Keys.Contains(value))
                        throw new InvalidOperationException($"List does not have {value}.");

                    var dialogItem = dialogItems[value];
                    dialogItem.Click();
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, bool openLookupPage = true)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

                if (driver.HasElement(By.Id(field)))
                {
                    var input = driver.FindElement(By.Id(field));
                    input.Click();

                    if (input.FindElement(By.ClassName("Lookup_RenderButton_td")) == null)
                        throw new InvalidOperationException($"Field: {field} is not lookup");

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_{field}_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    var dialogItem = dialogItems.Values.Last();

                    try
                    {
                        dialogItem.Click();
                    }
                    catch (OpenQA.Selenium.StaleElementReferenceException ex)
                    {
                        // Expected error
                    }
                }
                else
                    throw new InvalidOperationException($"Field: {field} Does not exist");

                return true;
            });
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

        public BrowserCommandResult<bool> SetValue(string field, bool check)
        {
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {

                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
            return this.Execute(GetOptions(""), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
            return this.Execute(GetOptions($"Set Value: {option.Name}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
            return this.Execute(GetOptions($"Set Conposite Control Value: {control.Id}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToQuickFindFrame();

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
        
    }
}