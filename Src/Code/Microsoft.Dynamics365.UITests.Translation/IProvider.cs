using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics365.UITests.Translation
{
    public interface IProvider
    {
        Event Search(string text);
    }
}
