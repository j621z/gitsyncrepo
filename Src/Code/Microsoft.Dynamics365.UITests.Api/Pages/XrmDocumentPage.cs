using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api.Pages
{
    /// <summary>
    /// The XRM Document Page that provides methods to interact with the browswer DOM. 
    /// </summary>
    /// <seealso cref="Microsoft.Dynamics365.UITests.Browser.BrowserPage" />
    public class XrmDocumentPage : BrowserPage
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="XrmDocumentPage"/> class.
        /// </summary>
        /// <param name="browser">The Interactive Browser session created with the API.</param>
        public XrmDocumentPage(InteractiveBrowser browser)
            : base(browser)
        {
           
        }
        internal BrowserCommandOptions GetOptions(string commandName)
        {
            return new BrowserCommandOptions(Constants.DefaultTraceSource,
                commandName,
                0,
                0,
                null,
                false,
                typeof(NoSuchElementException), typeof(StaleElementReferenceException));
        }
        /// <summary>
        /// Returns the document element that has the ID attribute with the specified value.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BrowserCommandResult<IWebElement> getElementById(string id)
        {
            return this.Execute(GetOptions($"Element Search by ID: {id}"), driver => driver.FindElement(By.Id(id)));
        }

        /// <summary>
        /// Returns the document element that has the CSS attribute with the specified value.
        /// </summary>
        /// <param name="css">The CSS.</param>
        /// <returns></returns>
        public BrowserCommandResult<IWebElement> getElementByCss(string css)
        {
            return this.Execute(GetOptions($"Element Search by ID: {css}"), driver => driver.FindElement(By.CssSelector(css)));
        }

        /// <summary>
        /// Returns the document element that has the specified XPath value.
        /// </summary>
        /// <param name="xpath">The xpath that is used to references nodes in the document.</param>
        /// <returns></returns>
        public BrowserCommandResult<IWebElement> getElementByXPath(string xpath)
        {
            return this.Execute(GetOptions($"Element Search by ID: {xpath}"), driver => driver.FindElement(By.XPath(xpath)));
        }





    }
}
