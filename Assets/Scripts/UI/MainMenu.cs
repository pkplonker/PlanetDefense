using System;
using StuartHeathTools;
using UnityEngine;

namespace UI
{
	public class MainMenu : CanvasGroupBase
	{
		private void Awake()
		{
			ShowUI();
		}

		public static void NewGame()
		{
			SFXController.instance.PlayUIClick();
			GameManager.Instance.ChangeState(GameState.NewGame);
		}

		public static void Settings()
		{
			SFXController.instance.PlayUIClick();
			UI.Settings.Instance.Show();
		}

		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Menu) ShowUI(2f);
			else HideUI(0f);
		}

		public static void Exit()
		{
			SFXController.instance.PlayUIClick();
			Debug.Log("Exit requested");
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
	}
}