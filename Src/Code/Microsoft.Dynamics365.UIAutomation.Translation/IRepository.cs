using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Translation
{
    public interface IRepository<TEvent, in TKey> where TEvent : class
    {
        IEnumerable<Event> GetAll();
        Event Get(string id);
        Event Search(string text);
    }
}
