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
			SFXController.instance.PlayUIClick();
			currentSpeed++;
			if ((int) currentSpeed >= Enum.GetNames(typeof(GameSpeed)).Length) currentSpeed = 0;
			UpdateCurrentSpeed(currentSpeed);
		}
		private void OnEnable() => GameManager.onStateChange += StateChange;
		private void OnDisable() => GameManager.onStateChange -= StateChange;
		private void StateChange(GameState state)
		{
			if (state != GameState.NewGame) return;
			currentSpeed = GameSpeed.Normal;
			UpdateCurrentSpeed(GameSpeed.Normal);
		}

		
		private void UpdateCurrentSpeed(GameSpeed requestedSpeed)
		{
			UpdateTime();
			UpdateUI();
		}

		private void UpdateTime()
		{
			Time.timeScale = currentSpeed switch
			{
				GameSpeed.Half => 0.5f,
				GameSpeed.Normal => 1f,
				GameSpeed.TwoTimes => 2f,
				GameSpeed.FourTimes => 4f,
				_ => Time.timeScale
			};
		}

		private void UpdateUI()
		{
			text.text = currentSpeed switch
			{
				GameSpeed.Half => "50%",
				GameSpeed.Normal => "100%",
				GameSpeed.TwoTimes => "200%",
				GameSpeed.FourTimes => "400%",
				_ => text.text
			};
		}
	}

	public enum GameSpeed
	{
		Half,
		Normal,
		TwoTimes,
		FourTimes
	}
}