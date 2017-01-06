using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Script.Serialization;
using System.Xml;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api
{
    public class XrmPerformanceCenterPage
        : BrowserPage
    {
        public XrmPerformanceCenterPage(InteractiveBrowser browser)
            : base(browser)
        {
        }

        internal const string GetAllMarkersJavascriptCommand = "return Mscrm.Performance.PerformanceMarkerManager.get_instance().get_allMarkersJson();";

        public bool IsEnabled
        {
            get
            {
                object value = this.Driver.ExecuteScript("return Mscrm.Performance.PerformanceMarkerManager.get_instance().get_isEnabled();");

                return (bool)value;
            }
            set
            {
                if (value)
                {
                    this.Driver.ExecuteScript("Mscrm.Performance.PerformanceMarkerManager.get_instance().set_isEnabled(true);");
                }
                else
                {
                    this.Driver.ExecuteScript("Mscrm.Performance.PerformanceMarkerManager.get_instance().set_isEnabled(false);");
                }
            }
        }

        private bool _performanceMarkersVisible = false;

        public BrowserCommandResult<bool> ToggleVisibility()
        {
            return this.Execute("Toggle Performance Marker Visibility", driver =>
            {
                if (IsEnabled)
                {
                    driver.ExecuteScript("Mscrm.Performance.PerformanceCenter.get_instance().TogglePerformanceResultsVisibility()");

                    if (!_performanceMarkersVisible)
                    {
                        driver.SwitchTo().ParentFrame();
                        driver.WaitUntilAvailable(By.Id("perfDiv"));

                        _performanceMarkersVisible = true;
                    }
                    else
                    {
                        this.GetPage<XrmNavigationPage>().SwitchToContentFrame();

                        _performanceMarkersVisible = false;
                    }
                }
                else
                {
                    throw new InvalidOperationException("Performance markers are not enabled and cannot be toggled.");
                }

                return true;
            });
        }

        public BrowserCommandResult<Dictionary<string, XrmPerformanceMarker>> GetMarkers()
        {
            return this.Execute("Get Performance Markers", driver =>
            {
                var jsonResults = driver.ExecuteScript(GetAllMarkersJavascriptCommand).ToString();
                var perfMarkers = new JavaScriptSerializer().Deserialize<Dictionary<string, XrmPerformanceMarker>>(jsonResults);
                var minTime = perfMarkers.Values.Min(v => v.Timestamp);

                foreach (var marker in perfMarkers)
                    marker.Value.MinTime = minTime;

                return perfMarkers;
            });
        }

        public BrowserCommandResult<Dictionary<string, object>> GetMarkersRaw()
        {
            return this.Execute("Get Performance Markers (raw)", driver =>
            {
                var jsonResults = driver.ExecuteScript(GetAllMarkersJavascriptCommand).ToString();
                var jsSerializer = new JavaScriptSerializer();

                jsSerializer.RegisterConverters(new[] { new DynamicJsonConverter() });

                var jsonObj = (Dictionary<string, object>)jsSerializer.Deserialize(jsonResults, typeof(Dictionary<string, object>));

                return jsonObj;
            });
        }

        public BrowserCommandResult<bool> SaveAs(string outputPath)
        {
            return SaveAs(outputPath, Newtonsoft.Json.Formatting.Indented);
        }

        public BrowserCommandResult<bool> SaveAs(string outputPath, Newtonsoft.Json.Formatting jsonFormatting)
        {
            return this.Execute("Save Performance Markers", driver =>
            {
                var jsonResults = driver.ExecuteScript(GetAllMarkersJavascriptCommand).ToString();
                var jsonFormattedString = JValue.Parse(jsonResults).ToString(jsonFormatting);

                using (FileStream fs = File.Open(outputPath, FileMode.CreateNew))
                {
                    using (StreamWriter sw = new StreamWriter(fs))
                    {
                        sw.Write(jsonFormattedString);
                    }
                }

                return true;
            });
        }

        public string RequestId
        {
            get
            {
                var requestId = this.Driver.ExecuteScript("return REQ_ID;").ToString();

                return requestId;
            }
        }
    }
}