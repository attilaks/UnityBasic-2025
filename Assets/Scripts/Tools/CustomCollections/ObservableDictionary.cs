using System;
using System.Collections.Generic;

namespace Tools.CustomCollections
{
	public class ObservableDictionary<TKey, TValue> 
	{
		public int Count => _dictionary.Count;
		
		public event Action<int> CountChanged = delegate { };
		public event Action<TKey, TValue> ValueUpdated = delegate { };
		
		private readonly Dictionary<TKey, TValue> _dictionary = new ();

		public TValue this[TKey index]
		{
			get => _dictionary[index];
			set
			{
				if (_dictionary.TryGetValue(index, out var currentValue))
				{
					_dictionary[index] = value;
					ValueUpdated.Invoke(index, value);
				}
				else
				{
					_dictionary[index] = value;
					CountChanged.Invoke(_dictionary.Count);
				}
			}
		}

		public void Add(TKey key, TValue value)
		{
			_dictionary.Add(key, value);
			CountChanged.Invoke(_dictionary.Count);
		}

		public bool Remove(TKey key)
		{
			var removed = _dictionary.Remove(key);
			if (removed)
				CountChanged.Invoke(_dictionary.Count);
			return removed;
		}

		public void Clear()
		{
			_dictionary.Clear();
			CountChanged.Invoke(_dictionary.Count);
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dictionary.TryGetValue(key, out value);
		}
	}
}