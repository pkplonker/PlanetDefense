using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
	public static SFXController instance;
	private AudioSource audioSource;
	[SerializeField] private AudioClip uiClick;

	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
	}

	private void Start()
	{
		audioSource = GetComponent<AudioSource>();
	}

	public void Playclip(AudioClip clip)
	{
		if (clip == null || audioSource == null) return;
		audioSource.PlayOneShot(clip);
	}

	public void PlayUIClick()
	{
		Playclip(uiClick);
	}
}