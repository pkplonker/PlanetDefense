using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StorySpeedToggles : MonoBehaviour
	{
		[SerializeField] private Image[] images;

		[SerializeField] private float[] speeds;

		[SerializeField] private int initialIndex;
		[SerializeField] private string storySpeedPlayerPrefsKey;
		private static List<StorySpeedToggles> toggles = new List<StorySpeedToggles>();

		private void Awake() => toggles.Add(this);

		private void Start()
		{
			if (images.Length != speeds.Length)
			{
				Debug.LogError("Mismatch of images and speeds");
			}

			PlayerPrefs.SetFloat(storySpeedPlayerPrefsKey, speeds[initialIndex]);
			Toggle(initialIndex);
		}

		private void UpdateAllToggles(int index)
		{
			foreach (var t in toggles)
			{
				t.ToggleValue(index);
			}
		}

		public void Toggle(int index)
		{
			UpdateAllToggles(index);
		}

		private void ToggleValue(int index)
		{
			if (index > images.Length)
			{
				Debug.LogError("incorrect story speed toggle index");
				return;
			}

			foreach (var i in images)
			{
				i.enabled = false;
			}

			images[index].enabled = true;
			PlayerPrefs.SetFloat(storySpeedPlayerPrefsKey, speeds[index]);
		}
	}
}