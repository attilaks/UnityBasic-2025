using System;
using System.Collections.Generic;
using UI;
using UnityEngine;

namespace Tools.Managers
{
	public class SoundManager : MonoBehaviour
	{
		[SerializeField] private List<AudioSource> soundEffects;
		[SerializeField] private List<AudioSource> music;

		private void Awake()
		{
			UIManager.SoundEffectsSliderValueChanged += OnSoundEffectsSliderValueChanged;
			UIManager.MusicSliderValueChanged += OnMusicSliderValueChanged;
		}

		private void OnMusicSliderValueChanged(float volume)
		{
			for (var i = 0; i < music.Count; i++)
			{
				music[i].volume = volume;
			}
		}

		private void OnSoundEffectsSliderValueChanged(float volume)
		{
			for (var i = 0; i < soundEffects.Count; i++)
			{
				soundEffects[i].volume = volume;
			}
		}
	}
}