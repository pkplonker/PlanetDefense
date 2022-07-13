using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class SpeedButton : MonoBehaviour
	{
		private static GameSpeed currentSpeed = GameSpeed.Normal;
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
			UpdateCurrentSpeed();
		}

		private void OnEnable() => GameManager.onStateChange += StateChange;
		private void OnDisable() => GameManager.onStateChange -= StateChange;

		private void StateChange(GameState state)
		{
			if (state != GameState.NewGame) return;
			currentSpeed = GameSpeed.Normal;
			UpdateCurrentSpeed();
		}

		public static float GetSpeedMultiplier()
		{
			return currentSpeed switch
			{
				GameSpeed.Half => 0.5f,
				GameSpeed.Normal => 1f,
				GameSpeed.TwoTimes => 2f,
				GameSpeed.FourTimes => 4f,
				_ => 1f
			};
		}

		private void UpdateCurrentSpeed()
		{
			UpdateTime();
			UpdateUI();
		}

		private void UpdateTime()
		{
			Time.timeScale = GetSpeedMultiplier();
		}

		private void UpdateUI()
		{
			text.text = currentSpeed switch
			{
				GameSpeed.Half => "50%",
				GameSpeed.Normal => "100%",
				GameSpeed.TwoTimes => "200%",
				GameSpeed.FourTimes => "400%",
#if UNITY_EDITOR
				GameSpeed.EightTimes => "800%",
				GameSpeed.SixteenTimes => "1600%",
#endif
				_ => text.text
			};
		}

		public void SetSpeedMultiplier(GameSpeed speed)
		{
			currentSpeed = speed;
			UpdateCurrentSpeed();
		}
	}

	public enum GameSpeed
	{
		Half,
		Normal,
		TwoTimes,
		FourTimes,
#if UNITY_EDITOR
		EightTimes,
		SixteenTimes
#endif
	}
}