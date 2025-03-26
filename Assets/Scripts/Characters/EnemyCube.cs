using System;
using System.Collections;
using Tools.Managers;
using UnityEngine;

namespace Characters
{
	[RequireComponent(typeof(BoxCollider))]
	[RequireComponent(typeof(Renderer))]
	[RequireComponent(typeof(HealthManager))]
	public class EnemyCube : MonoBehaviour
	{
		public event Action OnDeath = delegate { };
		public event Action<EnemyCube> OnDeathWithMe = delegate { };
		
		private HealthManager _healthManager;
		private Renderer _renderer;

		private void Awake()
		{
			_renderer = GetComponent<Renderer>();
			_healthManager = GetComponent<HealthManager>();
			_healthManager.DeathHasComeEvent += CubeIsDestroyed;
		}
		
		private void OnDestroy()
		{
			_healthManager.DeathHasComeEvent -= CubeIsDestroyed;
		}
		
		private void CubeIsDestroyed()
		{
			OnDeath.Invoke();
			OnDeathWithMe.Invoke(this);
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