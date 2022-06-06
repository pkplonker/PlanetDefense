using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
	public class SpeedButton : MonoBehaviour
	{
		private GameSpeed currentSpeed = GameSpeed.Normal;
		[SerializeField] private TextMeshProUGUI text;

		private void Start()
		{
			UpdateTime();
			UpdateUI();
		}
		
		public void Toggle()
		{
			currentSpeed++;
			if ((int) currentSpeed >= Enum.GetNames(typeof(GameSpeed)).Length)
			{
				currentSpeed = 0;
			}

			UpdateCurrentSpeed(currentSpeed);
		}

	

		private void UpdateCurrentSpeed(GameSpeed requestedSpeed)
		{
			UpdateTime();
			UpdateUI();
		}

		private void UpdateTime()
		{
			switch (currentSpeed)
			{
				
				case GameSpeed.Half:
					Time.timeScale = 0.5f;

					break;
				case GameSpeed.Normal:
					Time.timeScale = 1f;

					break;
				case GameSpeed.TwoTimes:
					Time.timeScale = 2f;

					break;
				
			}
		}

		private void UpdateUI()
		{
			switch (currentSpeed)
			{
				case GameSpeed.Half:
					text.text = "50%";

					break;
				case GameSpeed.Normal:
					text.text = "100%";

					break;
				case GameSpeed.TwoTimes:
					text.text = "200%";
					break;
			}
		}
	}

	public enum GameSpeed
	{
		Half,
		Normal,
		TwoTimes
		
	}
}