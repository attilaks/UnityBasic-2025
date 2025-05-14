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
	[RequireComponent(typeof(Rigidbody))]
	public class EnemyCube : MonoBehaviour
	{
		public event Action<int> OnDeath = delegate { };

		public int RuntimeId { get; private set; }
		private HealthManager _healthManager;
		private Renderer _renderer;
		private EnemyDeathEffectController _deathEffectVolume;
		private Rigidbody _rigidbody;
		
		private Color _originalColor;
		private Coroutine _deathCoroutine;

		private void Awake()
		{
			RuntimeId = GetInstanceID();
			_renderer = GetComponent<Renderer>();
			_healthManager = GetComponent<HealthManager>();
			_rigidbody = GetComponent<Rigidbody>();
			_deathEffectVolume = FindObjectOfType<EnemyDeathEffectController>();
			_originalColor = _renderer.material.color;
			
			_healthManager.DeathHasComeEvent += CubeIsDestroyed;
		}
		
		private void OnDestroy()
		{
			_healthManager.DeathHasComeEvent -= CubeIsDestroyed;
		}

		public void Restore(Vector3 position, Transform parent)
		{
			if (_deathCoroutine != null) StopCoroutine(_deathCoroutine);
			
			_rigidbody.velocity = Vector3.zero;
			_rigidbody.angularVelocity = Vector3.zero;
			gameObject.SetActive(true);
			
			_renderer.material.color = _originalColor;
			transform.position = position;
			transform.rotation = Quaternion.identity;
			transform.SetParent(parent);
			
			_healthManager.RestoreHealthToMax();
			
			for (var i = transform.childCount - 1; i >= 0; i--)
			{
				Destroy(transform.GetChild(i).gameObject);
			}
		}
		
		private void CubeIsDestroyed()
		{
			_deathCoroutine = StartCoroutine(FadeAway());
			OnDeath.Invoke(RuntimeId);
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
					gameObject.SetActive(false);
				}
				yield return new WaitForSeconds(0.1f);
			}
		}
	}
}