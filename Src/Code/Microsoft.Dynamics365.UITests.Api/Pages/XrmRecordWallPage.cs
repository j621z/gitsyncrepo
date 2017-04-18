using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Dynamics365.UITests.Browser;

namespace Microsoft.Dynamics365.UITests.Api.Pages
{
    public class XrmRecordWallPage: XrmPage
    {
        public XrmRecordWallPage(InteractiveBrowser browser)
            : base(browser)
        {

        }
        public BrowserCommandResult<bool> SelectTab(string entityName, Guid id)
        {
            return this.Execute(GetOptions($"Open: {entityName} {id}"), driver =>
            {
                return true;
            });
        }
    }
}
