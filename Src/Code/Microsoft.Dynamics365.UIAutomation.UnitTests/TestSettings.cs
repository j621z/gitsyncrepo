using Microsoft.Dynamics365.UIAutomation.Browser;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests
{
    public static class TestSettings
    {
        public static string AccountLogicalName = "account";
        public static string AccountId = "2EA7F14A-9935-E711-8102-FC15B4286D18";
        public static string InvalidAccountLogicalName = "accounts";

        public static string LookupField = "primarycontactid";
        public static string LookupName = "Rene Valdes (sample)";

        public static BrowserOptions Options = new BrowserOptions
                                                {
                                                    BrowserType = BrowserType.Firefox,
                                                    PrivateMode = true,
                                                    FireEvents = false
                                                };
    }
}
