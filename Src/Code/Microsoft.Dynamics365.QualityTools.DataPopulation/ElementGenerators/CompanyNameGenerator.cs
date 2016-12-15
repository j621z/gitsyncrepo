using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
    public class CompanyNameGenerator
        : DataElementGenerator<string, CompanyNameGenerationOptions>
    {
        public CompanyNameGenerator() 
            : base(CompanyNameGenerationOptions.Default)
        {
        }

        public CompanyNameGenerator(CompanyNameGenerationOptions options)
            : base(options)
        {
        }

        public override string Generate(CompanyNameGenerationOptions options)
        {
            if (options.CompanyNames != null && options.CompanyNames.Count > 0)
            {
                return options.CompanyNames.RandomItem();
            }
            else
            {
                return DataElementGenerator.Values.CompanyNames.RandomItem();
            }
        }
    }

    public class CompanyNameGenerationOptions
        : DataElementGenerationOptions
    {
        public CompanyNameGenerationOptions()
        {
            this.CompanyNames = new List<string>();
        }

        public CompanyNameGenerationOptions(List<string> companyNames)
        {
            this.CompanyNames = companyNames;
        }

        public List<string> CompanyNames { get; set; }

        public static CompanyNameGenerationOptions Default => new CompanyNameGenerationOptions();
    }

    public static partial class DataElementGenerator
    {
        public static partial class Values
        {
            public static List<string> CompanyNames = new List<string>()
            {
                "A. Datum Corporation",
                "AdventureWorks Cycles",
                "Allure Bays Corp.",
                "Alpine Ski House",
                "Awesome Computers",
                "Baldwin Museum of Science",
                "Blue Yonder Airlines",
                "City Power & Light",
                "Coho Vineyard",
                "Coho Winery",
                "Coho Vineyard & Winery",
                "Consolidated Messenger",
                "Contoso Bikes & Frozen Yogurt",
                "Contoso Bikes & Haircuts",
                "Contoso Bikes & Dogs",
                "Contoso Ltd.",
                "Contoso Pharmaceuticals",
                "Fabrikam, Inc.",
                "Fourth Coffee",
                "Graphic Design Institute",
                "Humongous Insurance",
                "Itexamworld.com",
                "LitWare Inc.",
                "Lucerne Publishing",
                "Margie's Travel",
                "Northridge Video",
                "Northwind Traders",
                "Parnell Aerospace",
                "Phone Company",
                "ProseWare, Inc.",
                "Reskit",
                "School of Fine Art",
                "Southbridge Video",
                "TailSpin Toys",
                "Tasmanian Traders",
                "The Phone Company",
                "Terra Firm",
                "Terra Flora, Inc.",
                "Trey Research Inc.",
                "WingTip Toys",
                "Wide World Importers",
                "Woodgrove Bank"
            };
        }
    }
}