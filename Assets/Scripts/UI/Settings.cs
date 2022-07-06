//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using StuartHeathTools;
using UnityEngine;
using UnityEngine.Audio;

namespace UI
{
	/// <summary>
	///Settings full description
	/// </summary>
	public class Settings : CanvasGroupBase
	{
		public static Settings Instance;
		[SerializeField] private AudioMixer audioMixer;

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

		private void Start() => HideUI();
		public void Back() => HideUI();

		public void Show() => ShowUI(0.5f);
		public void SetVolume(float volume, string channel, AudioMixer aMixer) => aMixer.SetFloat(channel, Mathf.Log10(volume) * 20);

		public void SetMasterVolume(float volume) => SetVolume(volume, "Master", audioMixer);
		public void SetMusicVolume(float volume) => SetVolume(volume, "Music", audioMixer);

		public void SetSFXVolume(float volume) => SetVolume(volume, "SFX", audioMixer);
		public void SetProjectilesVolume(float volume) => SetVolume(volume, "Projectiles", audioMixer);
		public void SetUIVolume(float volume) => SetVolume(volume, "UI", audioMixer);

	}
}