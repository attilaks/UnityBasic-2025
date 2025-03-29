using System;
using System.Collections.Generic;

namespace Tools.CustomCollections
{
	public class ObservableDictionary<TKey, TValue> 
	{
		public int Count => _dictionary.Count;
		public int PreviousCount { get; private set; }
		
		public event Action CountChanged = delegate { };
		public event Action<TKey, TValue> ValueUpdated = delegate { };
		
		private readonly Dictionary<TKey, TValue> _dictionary = new ();

		public TValue this[TKey index]
		{
			get => _dictionary[index];
			set
			{
				if (_dictionary.TryGetValue(index, out _))
				{
					_dictionary[index] = value;
					ValueUpdated.Invoke(index, value);
				}
				else
				{
					PreviousCount = _dictionary.Count;
					_dictionary[index] = value;
					CountChanged.Invoke();
				}
			}
		}

		public bool Remove(TKey key)
		{
			var removed = _dictionary.Remove(key);
			if (removed)
			{
				PreviousCount = _dictionary.Count + 1;
				CountChanged.Invoke();
			}
			return removed;
		}

		public void Clear()
		{
			PreviousCount = _dictionary.Count;
			_dictionary.Clear();
			CountChanged.Invoke();
		}

		public bool TryGetValue(TKey key, out TValue value)
		{
			return _dictionary.TryGetValue(key, out value);
		}
	}
}