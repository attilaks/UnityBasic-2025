using System;
using UnityEngine;

namespace Tools.Managers
{
	public class HealthManager
	{
		public HealthManager(float health)
		{
			Health = health;
		}
		
		public event Action DeathHasComeEvent = delegate { };
		public bool IsDead { get; private set; }

		public uint FirstAidKitCollected { get; private set; }

		private float _health;
		public float Health
		{
			get => _health;
			set
			{
				if (_health > value)
				{
					if (value <= 0f)
					{
						_health = 0f;
						IsDead = true;
						DeathHasComeEvent.Invoke();
					}
					else
					{
						_health = value;
						Debug.LogError($"Aaargh... I'm injured! My Health is {Health}");
					}
				}
				else
				{
					_health = value;
					IsDead = false;
				}
			}
		}

		public void GetFirstAidKit()
		{
			++FirstAidKitCollected;
		}
	}
}