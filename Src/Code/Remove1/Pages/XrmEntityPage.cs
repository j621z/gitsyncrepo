using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Microsoft.Dynamics365.UITests.Browser
{
    public class XrmEntityPage
        : BrowserPage
    {        
        public XrmEntityPage(InteractiveBrowser browser)
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

        private readonly string _navigateDownCssSelector = "img.recnav-down.ms-crm-ImageStrip-Down_Enabled_proxy";
        private readonly string _navigateUpCssSelector = "img.recnav-up.ms-crm-ImageStrip-Up_Enabled_proxy";

        public BrowserCommandResult<bool> OpenEntity(string entityName, Guid id)
        {
            return this.Execute(GetOptions($"Open: {entityName} {id}"), driver =>
            {
                var uri = new Uri(this.Driver.Url);
                var link = $"{uri.Scheme}://{uri.Authority}/main.aspx?etn={entityName}&pagetype=entityrecord&id=%7B{id:D}%7D";
                return OpenEntity(new Uri(link)).Value;
            });
        }

        public BrowserCommandResult<bool> OpenEntity(Uri uri)
        {
            return this.Execute($"Open: {uri}", driver =>
            {
                driver.Navigate().GoToUrl(uri);

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateDown()
        {
            //return this.Execute("Navigate Down", NavigateUpDown, _navigateDownCssSelector);
            return this.Execute(GetOptions("Navigate Down"), driver =>
            {
                if (!driver.HasElement(By.CssSelector(_navigateDownCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateDownCssSelector));

                buttons[0].Click();

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> NavigateUp()
        {
            //return this.Execute("Navigate Down", NavigateUpDown, _navigateUpCssSelector);
            return this.Execute(GetOptions("Navigate Up"), driver =>
            {
                if (!driver.HasElement(By.CssSelector(_navigateUpCssSelector)))
                    return false;

                var buttons = driver.FindElements(By.CssSelector(_navigateUpCssSelector));

                buttons[0].Click();

                driver.WaitFor(d => d.ExecuteScript(XrmPerformanceCenterPage.GetAllMarkersJavascriptCommand).ToString().Contains("AllSubgridsLoaded"));

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectForm(string name)
        {
            return this.Execute(GetOptions($"SelectForm: {name}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id("formselectorcontainer"))?.FindElement(By.TagName("a"))?.Click();
                var items = driver.FindElements(By.ClassName("ms-crm-FS-MenuItem-Title"));
                items.Where(x => x.Text == name).FirstOrDefault()?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectTab(string name)
        {
            return this.Execute(GetOptions($"SelectTab: {name}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                var sections = driver.FindElements(By.ClassName("ms-crm-InlineTabHeaderText"));
                sections.Where(x => x.FindElement(By.TagName("h2")).Text == name).FirstOrDefault()?.Click();
                return true;
            });
        }

        public BrowserCommandResult<bool> SelectLookup(string field, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver => 
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver=> 
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            return this.Execute(GetOptions($"Set Lookup Value: {field}"), driver=> 
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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

                    dialogItem?.Click();
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

        public BrowserCommandResult<bool> Popout()
        {
            return this.Execute(GetOptions($"Popout"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.ClassName("ms-crm-ImageStrip-popout"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridAddButton(string subgridName)
        {
            return this.Execute(GetOptions($"Click add button of {subgridName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id($"{subgridName}_addImageButton"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> ClickSubgridGridViewButton(string subgridName)
        {
            return this.Execute(GetOptions($"Click GridView button of {subgridName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                driver.FindElement(By.Id($"{subgridName}_openAssociatedGridViewImageButtonImage"))?.Click();

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, string value)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    var lookupIcon = input.FindElement(By.ClassName("Lookup_RenderButton_td"));
                    lookupIcon.Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (!dialogItems.Keys.Contains(value))
                        throw new InvalidOperationException($"List does not have {value}.");

                    var dialogItem = dialogItems[value];
                    dialogItem.Click();
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, [Range(0, 9)]int index)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();

                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    if (dialogItems.Count < index)
                        throw new InvalidOperationException($"List does not have {index + 1} items.");

                    var dialogItem = dialogItems.Values.ToList()[index];
                    dialogItem.Click();
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> SelectSubgridLookup(string subgridName, bool openLookupPage = true)
        {
            return this.Execute(GetOptions($"Set Lookup Value for Subgrid {subgridName}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                if (driver.HasElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}")))
                {
                    var input = driver.FindElement(By.Id($"inlineLookupControlForSubgrid_{subgridName}"));
                    input.Click();
                    
                    input.FindElement(By.ClassName("Lookup_RenderButton_td")).Click();

                    var dialogName = $"Dialog_lookup_{subgridName}_i_IMenu";
                    var dialog = driver.FindElement(By.Id(dialogName));

                    var dialogItems = OpenDialog(dialog).Value;

                    var dialogItem = dialogItems.Values.Last();

                    dialogItem?.Click();
                }

                return true;
            });
        }

        public BrowserCommandResult<bool> SetValue(string field, bool check)
        {
            //return this.Execute($"Set Value: {field}", SetValue, field, check);
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {

                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            return this.Execute(GetOptions($"Set Value: {field}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            //return this.Execute($"Set Value: {field.Value}", SetValue, field.Id, field.Value);
            return this.Execute(GetOptions(""), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            //return this.Execute($"Set Value: {option.Name}", SetValue, option);
            return this.Execute(GetOptions($"Set Value: {option.Name}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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
            //return this.Execute($"Set Conposite Control Value: {control.Id}", SetValue, control);
            return this.Execute(GetOptions($"Set Conposite Control Value: {control.Id}"), driver =>
            {
                this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

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