using OpenQA.Selenium;
using System.Collections.Generic;
using Microsoft.Dynamics365.UITests.Browser;


namespace Microsoft.Dynamics365.UITests.Api.Pages
{
    public class XrmRecorderPage : BrowserPage
    {

        public XrmRecorderPage(InteractiveBrowser browser)
            : base(browser)
        {
           
        }

        //public BrowserCommandResult<List<BrowserRecordingEvent>>  GetRecordedEvents()
        //{
        //    return this.Execute("Get Recorded Events", driver =>
        //    {
        //        return driver.GetJsonObject<List<BrowserRecordingEvent>>(Constants.Browser.Recording.GetRecordedEventsCommand);
        //    });
        //}

        //public bool IsScriptPresent(IWebDriver driver)
        //{
        //    return bool.Parse(driver.ExecuteScript(Constants.Browser.Recording.CheckIfScriptExistsScript).ToString());
        //}

    }
}
