namespace Microsoft.Dynamics365.QualityTools.DataPopulation.ElementGenerators
{
	public class AlphabetStringGenerator
		: StringGenerator<AlphabetStringGenerationOptions>
	{
		public AlphabetStringGenerator() 
			: base(AlphabetStringGenerationOptions.Default)
		{
		}

		public AlphabetStringGenerator(AlphabetStringGenerationOptions options)
			: base(options)
		{
		}
	}

	public class AlphabetStringGenerationOptions
		: StringGenerationOptions
	{
		private CapitalizationOptions _capitalizationOptions = CapitalizationOptions.MixedCase;

		public CapitalizationOptions CapitalizationOptions 
		{ 
			get 
			{
				return _capitalizationOptions; 
			} 
			set 
			{
				_capitalizationOptions = value;

				switch (_capitalizationOptions)
				{
					case CapitalizationOptions.MixedCase:
						this.AllowedCharacters = DataPopulation.DataElementGenerator.Values.AllLetters;

						break;
					case CapitalizationOptions.UpperCaseOnly:
						this.AllowedCharacters = DataPopulation.DataElementGenerator.Values.UpperCaseLetters;

						break;
					case CapitalizationOptions.LowerCaseOnly:
						this.AllowedCharacters = DataPopulation.DataElementGenerator.Values.LowerCaseLetters;

						break;
				}
			} 
		}

		public new static AlphabetStringGenerationOptions Default => new AlphabetStringGenerationOptions
		{
		    MinimumLength = StringGenerationOptions.Default.MinimumLength,
		    MaximumLength = StringGenerationOptions.Default.MaximumLength
		};
	}

	public enum CapitalizationOptions
	{
		MixedCase,
		LowerCaseOnly,
		UpperCaseOnly
	}
}
