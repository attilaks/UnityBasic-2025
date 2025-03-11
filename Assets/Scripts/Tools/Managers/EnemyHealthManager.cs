using System;
using UnityEngine;

namespace Tools.Managers
{
	public class EnemyHealthManager //: Component
	{
		public EnemyHealthManager(float health)
		{
			_health = health;
			IsDead = false;
		}
		
		public event Action DeathHasComeEvent = delegate { };
		public bool IsDead { get; private set; }

		private float _health;
		public float Health
		{
			get => _health;
			set
			{
				if (IsDead)
				{
					return;
				}
				
				if (value <= 0f)
				{
					_health = 0f;
					IsDead = true;
					DeathHasComeEvent.Invoke();
				}
				else
				{
					_health = value;
				}
			}
		}
	}
}