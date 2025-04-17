using System;
using System.Collections;
using Effects;
using Tools.Managers;
using UnityEngine;

namespace Characters
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(HealthManager))]
	public class EnemyCube : MonoBehaviour
	{
		public event Action<int> OnDeath = delegate { };

		public int RuntimeId { get; private set; }
		private HealthManager _healthManager;
		private Renderer _renderer;
		private EnemyDeathEffectController _deathEffectVolume;

		private void Awake()
		{
			RuntimeId = GetInstanceID();
			_renderer = GetComponent<Renderer>();
			_healthManager = GetComponent<HealthManager>();
			_deathEffectVolume = FindObjectOfType<EnemyDeathEffectController>();
			
			_healthManager.DeathHasComeEvent += CubeIsDestroyed;
		}
		
		private void OnDestroy()
		{
			_healthManager.DeathHasComeEvent -= CubeIsDestroyed;
		}
		
		private void CubeIsDestroyed()
		{
			OnDeath.Invoke(RuntimeId);
			StartCoroutine(FadeAway());
			if (_deathEffectVolume)
				_deathEffectVolume.TriggerEffect();
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