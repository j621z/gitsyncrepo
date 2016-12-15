using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation
{
	public static partial class DataElementGenerator
	{
		static DataElementGenerator()
		{
			Randomizer = new Random();
		}

		public static Random Randomizer { get; private set; }

		public static partial class Values
		{
			public static readonly string UpperCaseLetters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
			public static readonly string LowerCaseLetters = "abcdefghijklmnopqrstuvwxyz";
			public static readonly string AllLetters = UpperCaseLetters + LowerCaseLetters;
			public static readonly string Numbers = "0123456789";
		}

		#region Randomizer extensions

		public static bool NextBoolean(this Random random)
		{
			return (random.NextDouble() >= 0.5);
		}

		public static char NextChar(this Random random)
		{
			return Convert.ToChar(random.NextBoolean() ? random.NextSubstring(Values.UpperCaseLetters) : random.NextSubstring(Values.LowerCaseLetters));
		}

		public static string NextSubstring(this Random random, string @string)
		{
			return @string.Substring(random.Next(1, @string.Length - 1), 1);
		}

		public static T NextListItem<T>(this Random random, List<T> list)
		{
			return list[random.Next(list.Count)];
		}

		public static TKey NextKey<TKey, TValue>(this Random random, Dictionary<TKey, TValue> dictionary)
		{
			return dictionary.Keys.ElementAt(random.Next(dictionary.Keys.Count));
		}

		public static TValue NextValue<TKey, TValue>(this Random random, Dictionary<TKey, TValue> dictionary)
		{
			return dictionary.Values.ElementAt(random.Next(dictionary.Keys.Count));
		}

		#endregion

		#region Object extensions

		public static T RandomItem<T>(this List<T> list)
		{
			return Randomizer.NextListItem(list);
		}

		public static TKey RandomKey<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			return Randomizer.NextKey(dictionary);
		}

		public static TValue RandomValue<TKey, TValue>(this Dictionary<TKey, TValue> dictionary)
		{
			return Randomizer.NextValue(dictionary);
		}

		#endregion
	}

	public interface IDataElementGenerator
	{
	}

	public interface IDataElementGenerator<TReturn>
		: IDataElementGenerator
	{
		TReturn Generate();
	}

	public abstract class DataElementGenerator<TReturn> 
		: IDataElementGenerator<TReturn>
	{
		public abstract TReturn Generate();
	}

	public abstract class DataElementGenerator<TReturn, TOptions>
		: DataElementGenerator<TReturn>
		where TOptions : IDataElementGenerationOptions
	{
		public DataElementGenerator(TOptions options) : base()
		{
			this.Options = options;
		}

		public override TReturn Generate()
		{
			return Generate(this.Options);
		}

		public abstract TReturn Generate(TOptions options);

		public TOptions Options { get; private set; }
	}
}
