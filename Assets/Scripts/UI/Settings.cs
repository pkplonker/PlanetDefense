//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using StuartHeathTools;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace UI
{
	/// <summary>
	///Settings full description
	/// </summary>
	public class Settings : CanvasGroupBase
	{
		public static Settings Instance;
		[SerializeField] private AudioMixer audioMixer;
		[SerializeField] private Slider master;
		[SerializeField] private Slider sFX;
		[SerializeField] private Slider projectiles;
		[SerializeField] private Slider ui;
		[SerializeField] private Slider music;


		private void Awake()
		{
			if (Instance == this) return;

			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
				return;
			}

			Destroy(gameObject);
			HideUI();
		}

		private void Start()
		{
			HideUI();
			LoadAudioValuesFromPlayerPrefs();
		}

		public void Back() => HideUI();

		public void Show()
		{
			ShowUI(0.5f);
			LoadAudioValuesFromPlayerPrefs();
		}


		private void LoadAudioValuesFromPlayerPrefs()
		{
			if (PlayerPrefs.HasKey("Master"))
			{
				master.value = PlayerPrefs.GetFloat("Master");
			}
			else master.value = 1;

			if (PlayerPrefs.HasKey("Music"))
			{
				music.value = PlayerPrefs.GetFloat("Music");
			}
			else music.value = 1;

			if (PlayerPrefs.HasKey("SFX"))
			{
				sFX.value = PlayerPrefs.GetFloat("SFX");
			}
			else sFX.value = 1;

			if (PlayerPrefs.HasKey("UI"))
			{
				ui.value = PlayerPrefs.GetFloat("UI");
			}
			else ui.value = 1;

			if (PlayerPrefs.HasKey("Projectiles"))
			{
				projectiles.value = PlayerPrefs.GetFloat("Projectiles");
			}
			else projectiles.value = 1;
		}

		private void SetVolume(float volume, string channel, AudioMixer aMixer)
		{
			aMixer.SetFloat(channel, Mathf.Log10(volume) * 20);
			PlayerPrefs.SetFloat(channel, volume);
			PlayerPrefs.Save();
		}

		public void SetMasterVolume(float volume) => SetVolume(volume, "Master", audioMixer);
		public void SetMusicVolume(float volume) => SetVolume(volume, "Music", audioMixer);

		public void SetSFXVolume(float volume) => SetVolume(volume, "SFX", audioMixer);
		public void SetProjectilesVolume(float volume) => SetVolume(volume, "Projectiles", audioMixer);
		public void SetUIVolume(float volume) => SetVolume(volume, "UI", audioMixer);
	}
}