using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXController : MonoBehaviour
{
	public static SFXController instance;
	[SerializeField] private AudioSource generalSFX;
	[SerializeField] private AudioSource projectileSFX;
	[SerializeField] private AudioSource UISFX;

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

	public void PlayUIClick() => Playclip(uiClick, SFXType.UI);

	public void Playclip(AudioClip clip, SFXType type)
	{
		if (clip == null) return;
		switch (type)
		{
			case SFXType.SFX:
				generalSFX.PlayOneShot(clip);

				break;
			case SFXType.Projectile:
				projectileSFX.PlayOneShot(clip);

				break;
			case SFXType.UI:
				UISFX.PlayOneShot(clip);

				break;
			default:
				throw new ArgumentOutOfRangeException(nameof(type), type, null);
		}
	}

	public enum SFXType
	{
		SFX,
		Projectile,
		UI
	}
}