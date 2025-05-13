using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Effects
{
	[RequireComponent(typeof(Volume))]
	public class EnemyDeathEffectController : MonoBehaviour
	{
		[SerializeField] private float fadeInDuration = 0.1f; 
		[SerializeField] private float fadeOutDuration = 0.2f;
		[SerializeField] private float effectDelay = 0.1f;
		
		private Volume _volume;
		private ColorAdjustments _colorAdjustments;
		private Vignette _vignette;
		
		private float _originalSaturation;
		private float _originalVignetteIntensity;
		
		private void Awake()
		{
			_volume = GetComponent<Volume>();
			if (!_volume.profile.TryGet(out _colorAdjustments))
			{
				Debug.LogError("ColorGrading not found in Volume!");
				return;
			}

			if (!_volume.profile.TryGet(out _vignette))
			{
				Debug.LogError("Vignette not found in Volume!");
				return;
			}
			
			_originalSaturation = _colorAdjustments.saturation.value;
			_originalVignetteIntensity = _vignette.intensity.value;
		}

		public void TriggerEffect()
		{
			StartCoroutine(ApplyEffect());
		}

		private IEnumerator ApplyEffect()
		{
			var timer = 0f;
			Time.timeScale = 0.5f;
			while (timer < fadeInDuration)
			{
				timer += Time.deltaTime;
				var t = timer / fadeInDuration;
				_colorAdjustments.saturation.value = Mathf.Lerp(_originalSaturation, -100f, t);
				_vignette.intensity.value = Mathf.Lerp(_originalVignetteIntensity, 0.5f, t);
				yield return null;
			}
			
			yield return new WaitForSeconds(effectDelay);
			
			timer = 0f;
			Time.timeScale = 1f;
			while (timer < fadeOutDuration)
			{
				timer += Time.deltaTime;
				var t = timer / fadeOutDuration;
				_colorAdjustments.saturation.value = Mathf.Lerp(-100f, _originalSaturation, t);
				_vignette.intensity.value = Mathf.Lerp(0.5f, _originalVignetteIntensity, t);
				yield return null;
			}
			
			_colorAdjustments.saturation.value = _originalSaturation;
		}
	}
}