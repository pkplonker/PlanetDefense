using System;
using UnityEngine;

namespace UI
{
	public class GameOverUI : UICanvas
	{
		private void Start() => Hide(0f);
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
			switch (state)
			{
				case GameState.Dead:
					Show(3f);
					GameManager.ChangeState(GameState.GameOver);
					break;
				case GameState.GameOver:
					Show(3f);
					break;
				default:
					Hide(0);
					break;
			}
		}
	}
}