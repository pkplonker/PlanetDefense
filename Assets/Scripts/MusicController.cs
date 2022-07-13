//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using UnityEngine;

/// <summary>
///MusicController full description
/// </summary>
public class MusicController : MonoBehaviour
{
	[SerializeField] private AudioClip menuAudioClip;
	[SerializeField] private AudioClip gameAudioClip;
	private AudioSource audioSource;
	private void OnEnable() => GameManager.onStateChange += StateChange;
	private void OnDisable() => GameManager.onStateChange -= StateChange;
	private void Awake()
	{
		audioSource = GetComponent<AudioSource>();
		PlayClip(menuAudioClip);
	}


	private void StateChange(GameState state)
	{
		switch (state)
		{
			case GameState.Menu:
				PlayClip(menuAudioClip);
				break;
			case GameState.Dead:
				PlayClip(menuAudioClip);
				break;
			case GameState.Paused:
				PlayClip(menuAudioClip);
				break;
			case GameState.GameOver:
				PlayClip(menuAudioClip);
				break;
			case GameState.NewGame:
				PlayClip(menuAudioClip);
				break;
			case GameState.WaveOver:
				break;
			case GameState.Shop:
				PlayClip(gameAudioClip);
				break;
			case GameState.NewWave:
				break;
			case GameState.Complete:
				PlayClip(menuAudioClip);
				break;
			case GameState.InGame:
				PlayClip(gameAudioClip);
				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(state), state, null);
		}
	}

	private void PlayClip(AudioClip clip)
	{
		if(clip==null) audioSource.Stop();
		if (audioSource.clip == clip) return;
		audioSource.clip = clip;
		audioSource.Play();
	}
}