using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Tools.Managers
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private List<AudioSource> soundEffects;
		[SerializeField] private List<AudioSource> music;
		
		private const string MusicVolume = "MusicVolume";
		private const string SoundEffectVolume = "SoundEffectVolume";

		private void Awake()
		{
			if (PlayerPrefs.HasKey(MusicVolume))
			{
				for (var i = 0; i < music.Count; i++)
				{
					music[i].volume = PlayerPrefs.GetFloat(MusicVolume);
				}
			}

			if (PlayerPrefs.HasKey(SoundEffectVolume))
			{
				for (var i = 0; i < soundEffects.Count; i++)
				{
					soundEffects[i].volume = PlayerPrefs.GetFloat(SoundEffectVolume);
				}
			}
			
			UIManager.SoundEffectsSliderValueChanged += OnSoundEffectsSliderValueChanged;
			UIManager.MusicSliderValueChanged += OnMusicSliderValueChanged;
		}

		private void OnDestroy()
		{
			UIManager.SoundEffectsSliderValueChanged -= OnSoundEffectsSliderValueChanged;
			UIManager.MusicSliderValueChanged -= OnMusicSliderValueChanged;
		}

		private void OnMusicSliderValueChanged(float volume)
		{
			for (var i = 0; i < music.Count; i++)
			{
				music[i].volume = volume;
			}
			
			PlayerPrefs.SetFloat(MusicVolume, volume);
		}

		private void OnSoundEffectsSliderValueChanged(float volume)
		{
			for (var i = 0; i < soundEffects.Count; i++)
			{
				soundEffects[i].volume = volume;
			}
			
			PlayerPrefs.SetFloat(SoundEffectVolume, volume);
		}
	}
}