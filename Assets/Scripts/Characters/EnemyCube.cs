using System;
using Tools.Weapons;
using UnityEngine;

namespace Characters
{
	[RequireComponent(typeof(BoxCollider))]
	public class EnemyCube : MonoBehaviour
	{
		[SerializeField] private float maxHealth;
		
		public event Action OnDeath = delegate { };

		private void OnCollisionEnter(Collision other)
		{
			if (other.gameObject.TryGetComponent<Bullet45Acp>(out var bullet))
			{
				var damageDone = bullet.StandardDamage;
				Debug.LogError($"Damage done = {damageDone}");
			}
		}
	}
}