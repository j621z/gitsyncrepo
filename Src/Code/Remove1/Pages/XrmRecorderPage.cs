using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using Microsoft.Dynamics365.UITests.Browser;
using OpenQA.Selenium.Support.Events;
using OpenQA.Selenium.Support;


namespace Microsoft.Dynamics365.UITests.Browser.Pages
{
    public class XrmRecorderPage : BrowserPage
    {

        public XrmRecorderPage(InteractiveBrowser browser)
            : base(browser)
        {
           
        }

        public BrowserCommandResult<List<BrowserRecordingEvent>>  GetRecordedEvents()
        {
            return this.Execute("Get Recorded Events", driver =>
            {
                return driver.GetJsonObject<List<BrowserRecordingEvent>>(Constants.Browser.Recording.GetRecordedEventsCommand);
            });
        }

        public bool IsScriptPresent(IWebDriver driver)
        {
            return bool.Parse(driver.ExecuteScript(Constants.Browser.Recording.CheckIfScriptExistsScript).ToString());
        }

    }
}
