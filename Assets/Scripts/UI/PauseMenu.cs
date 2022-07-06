using StuartHeathTools;
using UnityEngine;

namespace UI
{
	public class PauseMenu : CanvasGroupBase
	{
		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			HideUI(0f);
		}

		public void Resume()
		{
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.InGame);
		}

		public void Restart()
		{
			SFXController.instance.PlayUIClick();
			MainMenu.NewGame();
		}

		public void Giveup()
		{
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.Dead);
		}

		public void Settings()
		{
			SFXController.instance.PlayUIClick();
			MainMenu.Settings();
		}

		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;


		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Paused) ShowUI(0f);
			else HideUI(0f);
		}
	}
}