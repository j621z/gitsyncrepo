using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UIAutomation.Translation
{
    public class EventRepository :  IRepository<Event, string>
    {
        private IProvider _provider;

        public EventRepository(IProvider provider)
        {
            _provider = provider;
        }
        public IEnumerable<Event> GetAll()
        {
            throw new NotImplementedException();
        }

        public Event Get(string id)
        {
            throw new NotImplementedException();
        }

        public Event Search(string text)
        {
            return _provider.Search(text);
        }
    }

}
