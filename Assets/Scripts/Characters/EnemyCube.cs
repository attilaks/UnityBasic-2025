using System;
using System.Collections;
using Tools.Managers;
using Tools.Weapons;
using UnityEngine;

namespace Characters
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Renderer))]
	public class EnemyCube : MonoBehaviour
	{
		[SerializeField] private float maxHealth = 50f;
		
		public event Action OnDeath = delegate { };
		
		private EnemyHealthManager _healthManager;
		private Renderer _renderer;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			_healthManager = new EnemyHealthManager(maxHealth);
			_healthManager.DeathHasComeEvent += CubeIsDestroyed;
		}

		// private void OnCollisionEnter(Collision other)
		// {
		// 	if (_healthManager.IsDead)
		// 	{
		// 		return;
		// 	}
		// 	
		// 	if (other.gameObject.TryGetComponent<Bullet45Acp>(out var bullet))
		// 	{
		// 		_healthManager.Health -= bullet.StandardDamage;
		// 	}
		// }
		
		private void OnDestroy()
		{
			_healthManager.DeathHasComeEvent -= CubeIsDestroyed;
		}
		
		private void CubeIsDestroyed()
		{
			OnDeath.Invoke();
			StartCoroutine(FadeAway());
		}

		private IEnumerator FadeAway()
		{
			var color = _renderer.material.color;
			while (color.a > 0)
			{
				color.a -= 0.05f;
				_renderer.material.color = color;
				if (color.a <= 0)
				{
					Destroy(gameObject);
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}