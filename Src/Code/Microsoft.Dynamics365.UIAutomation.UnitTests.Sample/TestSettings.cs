using Microsoft.Dynamics365.UIAutomation.Browser;
using System;

namespace Microsoft.Dynamics365.UIAutomation.UnitTests.Sample
{
    public static class TestSettings
    {
        public static string AccountLogicalName = "account";
        public static string AccountId = "BD8AC246-2416-E711-8104-FC15B4282DF4";
        public static string InvalidAccountLogicalName = "accounts";

        public static string LookupField = "primarycontactid";
        public static string LookupName = "Rene Valdes (sample)";
        private readonly static string browserType = System.Configuration.ConfigurationManager.AppSettings["BrowserType"].ToString();

        public static BrowserOptions Options = new BrowserOptions
        {
            BrowserType = (BrowserType)Enum.Parse(typeof(BrowserType), browserType),
            PrivateMode = true,
            FireEvents = true
        };
    }
}
