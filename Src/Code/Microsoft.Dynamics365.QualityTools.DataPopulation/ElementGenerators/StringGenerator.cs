using System;
using System.Text;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
	public class StringGenerator
		: StringGenerator<StringGenerationOptions>
	{
		public StringGenerator()
			: base(StringGenerationOptions.Default)
		{
		}

		public StringGenerator(StringGenerationOptions options)
			: base(options)
		{
		}
	}

	public class StringGenerator<TOptions>
		: DataElementGenerator<string, TOptions>
		where TOptions : StringGenerationOptions
	{
		public StringGenerator() 
			: base((TOptions)StringGenerationOptions.Default)
		{
		}

		public StringGenerator(TOptions options)
			: base(options)
		{
		}

		public override string Generate(TOptions options)
		{
			var builder = new StringBuilder();
			var sourceCharacters = options.AllowedCharacters;

			for (var i = 0; i < DataPopulation.DataElementGenerator.Randomizer.Next(options.MinimumLength, options.MaximumLength); i++)
			{
				builder.Append(DataPopulation.DataElementGenerator.Randomizer.NextSubstring(sourceCharacters));
			}

			return builder.ToString();
		}
	}

	public class StringGenerationOptions
		: DataElementGenerationOptions
	{
		public string AllowedCharacters { get; set; }
		public Int32 MinimumLength { get; set; }
		public Int32 MaximumLength { get; set; }

		public static StringGenerationOptions Default => new StringGenerationOptions 
		{
		    MinimumLength = 1, 
		    MaximumLength = 30, 
		    AllowedCharacters = DataPopulation.DataElementGenerator.Values.AllLetters 
		};
	}
}
