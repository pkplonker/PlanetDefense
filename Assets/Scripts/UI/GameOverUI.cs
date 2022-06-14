using System;
using UnityEngine;

namespace UI
{
	public class GameOverUI : UICanvas
	{
		private void Start() => Hide();
		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;

		public void Restart()
		{
			GameManager.ChangeState(GameState.NewGame);
			SFXController.instance.PlayUIClick();
		}

		public void Menu()
		{
			GameManager.ChangeState(GameState.Menu);
			SFXController.instance.PlayUIClick();
		} 

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Dead)
			{
				Show();
				GameManager.ChangeState(GameState.GameOver);
			}
			else Hide();
		}
	}
}