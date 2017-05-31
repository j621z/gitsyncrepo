using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Translation
{
    public class EventCollection
    {
        public List<Event> Events { get; set; }
    }

    public class Event : IEvent
    {
        public string ElementId { get; set; }
        public string CssClass { get; set; }
        public string XPath { get; set; }
        public string ApiCall { get; set; }
        public string Name { get; set; }
        public int Id { get; set; }
    }
}
