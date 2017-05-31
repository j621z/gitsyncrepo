using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Translation
{
    internal static class EventReference
    {

        internal static Dictionary<string, int> Elements = new Dictionary<string, int>()
            {
                { "HomeTabLink", 200000001 },
                { "homeButtonImage navTabButtonImageSandbox", 200000001 },
            };

        internal static Dictionary<int, Event> EventCollection = new Dictionary<int, Event>()
            {
                { 200000001, new Event() {Id=200000001,Name="Home Button",ElementId="HomeTabLink",CssClass="a#HomeTabLink",XPath="id{\"HomeTabLink\"}",ApiCall=""} },
                //{ 200000002, new Event() {Id=200000001,Name="Home Button",ElementId="homeButtonImage navTabButtonImageSandbox",CssClass="img#homeButtonImage navTabButtonImageSandbox",XPath="id{\"homeButtonImage navTabButtonImageSandbox\"}",ApiCall=""}}
            };
    }
}
