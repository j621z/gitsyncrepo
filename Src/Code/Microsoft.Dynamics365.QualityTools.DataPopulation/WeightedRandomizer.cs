using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Dynamics365.QualityTools.DataPopulation
{
	public class WeightedRandomizer<T>
	{
		private readonly List<WeightedItem<T>> _items = new List<WeightedItem<T>>();
		private int _currentWeight = 0;
		private readonly object _sync = new object();

		public void Add(Dictionary<T, Int32> items)
		{
			foreach (var i in items)
			{
				Add(i.Value, i.Key);
			}
		}

		public void Add(Int32 weight, T item)
		{
			_items.Add(new WeightedItem<T> 
				{ 
					MaximumWeight = _currentWeight, 
					MinimumWeight = _currentWeight + weight, 
					Item = item 
				});

			lock (_sync)
			{
				_currentWeight += (weight + 1);
			}
		}

		public T Next()
		{
			Int32 randomWeight = DataElementGenerator.Randomizer.Next(0, _currentWeight);

			return _items.First(i => i.MinimumWeight >= randomWeight && i.MaximumWeight <= randomWeight).Item;
		}

		private class WeightedItem<TItem>
		{
			public Int32 MinimumWeight { get; set; }
			public Int32 MaximumWeight { get; set; }
			public TItem Item { get; set; }
		}
	}
}
