using System;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
	public class Int32Generator
		: DataElementGenerator<Int32, Int32GeneratorOptions>
	{
		public Int32Generator() 
			: base(Int32GeneratorOptions.Default)
		{
		}

		public Int32Generator(Int32GeneratorOptions options)
			: base(options)
		{
		}

		public override int Generate(Int32GeneratorOptions options)
		{
			return DataPopulation.DataElementGenerator.Randomizer.Next(options.MinimumValue, options.MaximumValue);
		}
	}

	public class Int32GeneratorOptions
		: DataElementGenerationOptions
	{
		public Int32 MinimumValue { get; set; }
		public Int32 MaximumValue { get; set; }

		public static Int32GeneratorOptions Default => new Int32GeneratorOptions { MinimumValue = Int32.MinValue, MaximumValue = Int32.MaxValue };
	}
}
