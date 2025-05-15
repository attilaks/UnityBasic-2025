using System;
using UnityEngine;

namespace Tools.Managers
{
	public class HealthManager : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 50f;
		
		public event Action DeathHasComeEvent = delegate { };
		private bool _isDead;
		private float _health;

		private void Awake()
		{
			_health = maxHealth;
		}

		private float Health
		{
			get => _health;
			set
			{
				if (_isDead)
				{
					return;
				}
				
				if (value <= 0f)
				{
					_health = 0f;
					_isDead = true;
					DeathHasComeEvent.Invoke();
				}
				else
				{
					_health = value;
				}
			}
		}

		public void TakeDamage(float damage)
		{
			Health -= damage;
		}

		public void RestoreHealthToMax()
		{
			_isDead = false;
			Health = maxHealth;
		}
	}
}