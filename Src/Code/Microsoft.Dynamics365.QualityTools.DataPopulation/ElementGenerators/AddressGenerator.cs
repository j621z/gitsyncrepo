using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
    public class AddressGenerator
        : DataElementGenerator<Address, AddressGeneratorOptions>
    {
        private static readonly Int32Generator _addressNumberGenerator = new Int32Generator(new Int32GeneratorOptions { MinimumValue = 1, MaximumValue = 200000 });
        private static readonly Int32Generator _addressLine2NumberGenerator = new Int32Generator(new Int32GeneratorOptions { MinimumValue = 1, MaximumValue = 99 });
        private static readonly GivenNameGenerator _givenNameGenerator = new GivenNameGenerator();
        private static readonly SurnameGenerator _surnameGenerator = new SurnameGenerator();
        private static readonly StringGenerator _postalCodeGenerator = new StringGenerator(new StringGenerationOptions 
        { 
            MinimumLength = 5, 
            MaximumLength = 5, 
            AllowedCharacters = DataPopulation.DataElementGenerator.Values.Numbers 
        });

        static AddressGenerator()
        {
        }

        public AddressGenerator() 
            : base(AddressGeneratorOptions.Default)
        {
        }

        public AddressGenerator(AddressGeneratorOptions options)
            : base(options)
        {
        }

        public override Address Generate(AddressGeneratorOptions options)
        {
            string line1 = string.Format("{0} {1} {2}", 
                _addressNumberGenerator.Generate(), 
                DataElementGenerator.Values.StreetNames.RandomItem(), 
                options.UseShortNames 
                    ? DataElementGenerator.Values.StreetSuffixes.RandomValue() 
                    : DataElementGenerator.Values.StreetSuffixes.RandomKey());

            string line2 = "";
            string line3 = "";

            if (options.IncludeLine2)
            {
                line2 = string.Format("{1} {0}",
                    _addressLine2NumberGenerator.Generate(),
                    options.UseShortNames
                        ? DataElementGenerator.Values.AddressLine2Prefixes.RandomValue()
                        : DataElementGenerator.Values.AddressLine2Prefixes.RandomKey());
            }

            if (options.IncludeLine3)
            {
                line3 = $"C/O {_givenNameGenerator.Generate()} {_surnameGenerator.Generate()}";
            }

            return new Address()
            {
                Line1 = options.IncludeLine3 ? line3 : line1,
                Line2 = options.IncludeLine3 ? line1 : line2,
                Line3 = options.IncludeLine3 ? line2 : line3,
                City = DataElementGenerator.Values.CityNames.RandomItem(),
                StateProvince = options.UseShortNames 
                    ? DataElementGenerator.Values.StateNames.RandomValue() 
                    : DataElementGenerator.Values.StateNames.RandomKey(),
                PostalCode = _postalCodeGenerator.Generate(),
                Country = options.UseShortNames ? "USA" : "United States of America"
            };
        }
    }

    public class Address
    {
        public string Line1 { get; set; }
        public string Line2 { get; set; }
        public string Line3 { get; set; }
        public string City { get; set; }
        public string StateProvince { get; set; }
        public string PostalCode { get; set; }
        public string Country { get; set; }
    }

    public class AddressGeneratorOptions
        : DataElementGenerationOptions
    {
        public bool IncludeLine2 { get; set; }
        public bool IncludeLine3 { get; set; }
        public bool UseShortNames { get; set; }

        public static AddressGeneratorOptions Default
        {
            get
            {
                bool addLine2 = DataPopulation.DataElementGenerator.Randomizer.NextBoolean();
                bool addLine3 = addLine2 && DataPopulation.DataElementGenerator.Randomizer.NextBoolean();

                return new AddressGeneratorOptions
                {
                    IncludeLine2 = addLine2,
                    IncludeLine3 = addLine3,
                    UseShortNames = DataPopulation.DataElementGenerator.Randomizer.NextBoolean()
                };
            }
        }
    }

    public static partial class DataElementGenerator
    {
        public static partial class Values
        {
            public static Dictionary<string, string> AddressLine2Prefixes = new Dictionary<string, string>()
            {
                { "Apartment", "Apt." },
                { "Building", "Bldg." },
                { "Suite", "Ste." },
                { "PO Box", "Box "}
            };

            public static List<string> StreetNames = new List<string>()
            {
                "Oak",
                "Main",
                "Maple",
                "Walnut",
                "First",
                "Second",
                "Third",
                "Fourth",
                "Fifth",
                "Park",
                "Sixth",
                "Seventh",
                "Pine",
                "Cedar",
                "Eighth",
                "Washington",
                "Ninth",
                "Lake",
                "Hill",
                "Market",
                "Broadway",
                "Lincoln",
                "Central",
                "Hickory",
                "Taylor",
                "Wall",
                "Kennedy",
                "Jefferson",
                "Taft",
                "Grant",
                "Beach",
                "Laurel",
                "Kingsway",
                "Kings",
                "Parsons",
                "State",
                "Roosevelt",
                "Wood",
                "Forest",
                "Country Acres",
                "Tyler",
                "Birch",
                "Spruce",
                "Sycamore",
                "Douglas",
                "Congress",
                "Martin Luthar King",
                "Amber",
                "Auburn",
                "Bent",
                "Big",
                "Birch",
                "Blue",
                "Bright",
                "Broad",
                "Burning",
                "Calm",
                "Cinder",
                "Clear",
                "Cold",
                "Colonial",
                "Cool",
                "Cotton",
                "Cozy",
                "Crimson",
                "Crystal",
                "Dewy",
                "Dusty",
                "Easy",
                "Emerald",
                "Fallen",
                "Foggy",
                "Gentle",
                "Golden",
                "Grand",
                "Green",
                "Happy",
                "Harvest",
                "Hazy",
                "Heather",
                "Hidden",
                "High",
                "Honey",
                "Hush",
                "Indian",
                "Iron",
                "Ivory",
                "Jagged",
                "Lazy",
                "Little",
                "Lone",
                "Lonely",
                "Long",
                "Lost",
                "Merry",
                "Middle",
                "Misty",
                "Noble",
                "Old",
                "Orange",
                "Pearl",
                "Pied",
                "Pleasant",
                "Pretty",
                "Quaint",
                "Quaking",
                "Quiet",
                "Red",
                "Rocky",
                "Rose",
                "Rough",
                "Round",
                "Rustic",
                "Sandy",
                "Shady",
                "Silent",
                "Silver",
                "Sleepy",
                "Small",
                "Square",
                "Still",
                "Stony",
                "Strong",
                "Sunny",
                "Sweet",
                "Tawny",
                "Tender",
                "Thunder",
                "Turning",
                "Twin",
                "Umber",
                "Velvet",
                "White",
                "Windy",
                "Acorn",
                "Anchor",
                "Apple",
                "Autumn",
                "Axe",
                "Barn",
                "Beacon",
                "Bear",
                "Beaver",
                "Berry",
                "Bird",
                "Blossom",
                "Bluff",
                "Branch",
                "Bridge",
                "Brook",
                "Butterfly",
                "Butternut",
                "Castle",
                "Chestnut",
                "Cider",
                "Cloud",
                "Cottage",
                "Creek",
                "Crow",
                "Dale",
                "Deer",
                "Diamond",
                "Dove",
                "Elk",
                "Elm",
                "Embers",
                "Fawn",
                "Feather",
                "Flower",
                "Forest",
                "Fox",
                "Gate",
                "Goat",
                "Goose",
                "Grove",
                "Harbor",
                "Hickory",
                "Hills",
                "Holly",
                "Horse",
                "Island",
                "Lake",
                "Lamb",
                "Leaf",
                "Log",
                "Maple",
                "Mill",
                "Mountain",
                "Nectar",
                "Nest",
                "Nut",
                "Oak",
                "Panda",
                "Peach",
                "Pebble",
                "Pine",
                "Pioneer",
                "Pond",
                "Pony",
                "Prairie",
                "Pumpkin",
                "Quail",
                "Rabbit",
                "Rise",
                "River",
                "Robin",
                "Rock",
                "Shadow",
                "Sky",
                "Snake",
                "Spring",
                "Squirrel",
                "Stone",
                "Swan",
                "Timber",
                "Treasure",
                "Turtle",
                "View",
                "Wagon",
                "Willow",
                "Zephyr",
                "Acres",
                "Alcove",
                "Arbor",
                "Avenue",
                "Bank",
                "Bayou",
                "Bend",
                "Bluff",
                "Byway",
                "Canyon",
                "Chase",
                "Circle",
                "Corner",
                "Court",
                "Cove",
                "Crest",
                "Cut",
                "Dale",
                "Dell",
                "Drive",
                "Edge",
                "Estates",
                "Falls",
                "Farms",
                "Field",
                "Flats",
                "Gardens",
                "Gate",
                "Glade",
                "Glen",
                "Grove",
                "Haven",
                "Heights",
                "Highlands",
                "Hollow",
                "Isle",
                "Jetty",
                "Journey",
                "Knoll",
                "Lace",
                "Lagoon",
                "Landing",
                "Lane",
                "Ledge",
                "Manor",
                "Meadow",
                "Mews",
                "Niche",
                "Nook",
                "Orchard",
                "Pace",
                "Park",
                "Pass",
                "Path",
                "Pike",
                "Place",
                "Point",
                "Promenade",
                "Quay",
                "Race",
                "Ramble",
                "Ridge",
                "Road",
                "Round",
                "Rove",
                "Run",
                "Saunter",
                "Shoal",
                "Stead",
                "Street",
                "Stroll",
                "Summit",
                "Swale",
                "Terrace",
                "Trace",
                "Trail",
                "Trek",
                "Turn",
                "Twist",
                "Vale",
                "Valley",
                "View",
                "Villa",
                "Vista",
                "Wander",
                "Way",
                "Woods"
            };

            public static Dictionary<string, string> StreetSuffixes = new Dictionary<string, string>()
            {
                { "Avenue", "Ave." },
                { "Bend", "Bnd." },
                { "Boulevard", "Blvd." },
                { "Bypass", "Byp." },
                { "Causeway", "Cswy." },
                { "Circle", "Cir." },
                { "Court", "Ct." },
                { "Drive", "Dr." },
                { "Expressway", "Expy." },
                { "Freeway", "Fwy." },
                { "Junction", "Jct." },
                { "Lane", "Ln." },
                { "Parkway", "Pkwy." },
                { "Pass", "Pass" },
                { "Place", "Pl." },
                { "Road", "Rd." },
                { "Street", "St." },
                { "Terrace", "Ter." },
                { "Way", "Way" }
            };

            public static List<string> CityNames = new List<string>()
            {
                "Greenvlle",
                "Franklin",
                "Bristol",
                "Clinton",
                "Springfield",
                "Fairview",
                "Salem",
                "Washington",
                "Madison",
                "Georgetown"
            };

            public static Dictionary<string, string> StateNames = new Dictionary<string, string>()
            {
                { "AL", "Alabama" },
                { "AK", "Alaska" },
                { "AZ", "Arizona" },
                { "CA", "California" },
                { "CO", "Colorado" },
                { "CT", "Connecticut" },
                { "DE", "Delaware" },
                { "FL", "Florida" },
                { "GA", "Georgia" },
                { "HI", "Hawaii" },
                { "ID", "Idaho" },
                { "IL", "Illinois" },
                { "IN", "Indiana" },
                { "IA", "Iowa" },
                { "KS", "Kansas" },
                { "KY", "Kentucky" },
                { "LA", "Louisiana" },
                { "ME", "Maine" },
                { "MD", "Maryland" },
                { "MA", "Massachusetts" },
                { "MI", "Michigan" },
                { "MN", "Minnesota" },
                { "MS", "Mississippi" },
                { "MO", "Missouri" },
                { "MT", "Montana" },
                { "NE", "Nebraska" },
                { "NV", "Nevada" },
                { "NH", "New Hampshire" },
                { "NJ", "New Jersey" },
                { "NM", "New Mexico" },
                { "NY", "New York" },
                { "NC", "North Carolina" },
                { "ND", "North Dakota" },
                { "OH", "Ohio" },
                { "OK", "Oklahoma" },
                { "OR", "Oregon" },
                { "PA", "Pennsylvania" },
                { "RI", "Rhode Island" },
                { "SC", "South Carolina" },
                { "SD", "South Dakota" },
                { "TN", "Tennessee" },
                { "TX", "Texas" },
                { "UT", "Utah" },
                { "VT", "Vermont" },
                { "VA", "Virginia" },
                { "WA", "Washington" },
                { "WV", "West Virginia" },
                { "WI", "Wisconsin" },
                { "WY", "Wyoming" }
            };
        }
    }
}