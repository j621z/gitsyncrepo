using System.Collections.Generic;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
    public class EmailAddressGenerator
        : DataElementGenerator<string, EmailAddressGenerationOptions>
    {
        private static readonly GivenNameGenerator _givenNameGenerator = new GivenNameGenerator();
        private static readonly SurnameGenerator _surnameGenerator = new SurnameGenerator();
        private static readonly StringGenerator _stringGenerator = new StringGenerator(new StringGenerationOptions { MinimumLength = 2, MaximumLength = 10, AllowedCharacters = DataPopulation.DataElementGenerator.Values.LowerCaseLetters });

        static EmailAddressGenerator()
        {
        }

        public EmailAddressGenerator()
            : base(EmailAddressGenerationOptions.Default)
        {
        }

        public EmailAddressGenerator(EmailAddressGenerationOptions options)
            : base(options)
        {
        }

        public override string Generate(EmailAddressGenerationOptions options)
        {
            string alias = "";
            string domain = options.DomainName;

            if (options.UseFirstNameLastNameAlias)
            {
                alias = string.Format("{0}.{1}", _givenNameGenerator.Generate().ToLower(), _surnameGenerator.Generate().ToLower());
            }
            else
            {
                alias = _stringGenerator.Generate();
            }

            if (string.IsNullOrEmpty(domain))
            {
                if (options.UseSubdomains)
                {
                    domain = string.Format("{0}.{1}.{2}",
                        _stringGenerator.Generate(),
                        DataElementGenerator.Values.DomainNames.RandomItem(),
                        DataElementGenerator.Values.TopLevelDomainNames.RandomItem());
                }
                else
                {
                    domain = string.Format("{0}.{1}",
                        DataElementGenerator.Values.DomainNames.RandomItem(),
                        DataElementGenerator.Values.TopLevelDomainNames.RandomItem());
                }
            }
            else
            {
                if (options.UseSubdomains)
                {
                    domain = string.Format("{0}.{1}",
                        _stringGenerator.Generate(),
                        domain);
                }
            }

            return string.Format("{0}@{1}", alias, domain);
        }
    }

    public class EmailAddressGenerationOptions
        : DataElementGenerationOptions
    {
        public bool UseFirstNameLastNameAlias { get; set; }

        public bool UseSubdomains { get; set; }

        public string DomainName { get; set; }

        public static EmailAddressGenerationOptions Default => new EmailAddressGenerationOptions() { UseFirstNameLastNameAlias = false, UseSubdomains = false };
    }

    public static partial class DataElementGenerator
    {
        public static partial class Values
        {
            public static readonly List<string> DomainNames = new List<string>()
            {
                "example",
                "test",
                "random",
                "sample"
            };

            public static readonly List<string> TopLevelDomainNames = new List<string>()
            {
                "com",
                "net",
                "org",
                "intl",
                "edu",
                "gov",
                "mil"
            };
        }
    }
}