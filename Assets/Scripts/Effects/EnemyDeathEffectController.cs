using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace Effects
{
	[RequireComponent(typeof(PostProcessVolume))]
	public class EnemyDeathEffectController : MonoBehaviour
	{
		[SerializeField] private float fadeInDuration = 0.5f; 
		[SerializeField] private float fadeOutDuration = 0.5f;
		[SerializeField] private float effectDelay = 0.1f;
		
		private PostProcessVolume _volume;
		private ColorGrading _colorGrading;
		private float _originalSaturation;
		
		private void Awake()
		{
			_volume = GetComponent<PostProcessVolume>();
			if (!_volume.profile.TryGetSettings(out _colorGrading))
			{
				Debug.LogError("ColorGrading not found in PostProcessVolume!");
				return;
			}
			_originalSaturation = _colorGrading.saturation.value;
		}

		public void TriggerEffect()
		{
			StartCoroutine(ApplyEffect());
		}

		private IEnumerator ApplyEffect()
		{
			// Плавный уход в ч/б (Saturation: 0 → -100)
			var timer = 0f;
			while (timer < fadeInDuration)
			{
				timer += Time.deltaTime;
				var t = timer / fadeInDuration;
				_colorGrading.saturation.value = Mathf.Lerp(_originalSaturation, -100f, t);
				Debug.Log($"Saturation: {_colorGrading.saturation.value}");
				yield return null;
			}

			// Краткая задержка в ч/б
			yield return new WaitForSeconds(effectDelay);

			// Плавный возврат цвета (Saturation: -100 → original)
			timer = 0f;
			while (timer < fadeOutDuration)
			{
				timer += Time.deltaTime;
				var t = timer / fadeOutDuration;
				_colorGrading.saturation.value = Mathf.Lerp(-100f, _originalSaturation, t);
				Debug.Log($"Saturation: {_colorGrading.saturation.value}");
				yield return null;
			}

			// Фикс на случай ошибок округления
			_colorGrading.saturation.value = _originalSaturation;
			Debug.Log($"Saturation: {_colorGrading.saturation.value}");
		}
	}
}