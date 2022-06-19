using UnityEngine;

namespace UI
{
	public class PauseMenu : UICanvas
	{
		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			Hide();
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
			if (state == GameState.Paused) Show();
			else Hide();
		}
	}
}