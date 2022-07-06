using System;
using StuartHeathTools;
using UnityEngine;

namespace UI
{
	public class GameOverUI : CanvasGroupBase
	{
		private void Start() => HideUI(0f);
		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;

		public void Restart()
		{
			GameManager.ChangeState(GameState.NewGame);
			HideUI(0.3f);
			SFXController.instance.PlayUIClick();
		}

		public void Menu()
		{
			GameManager.ChangeState(GameState.Menu);
			HideUI(0.3f);
			SFXController.instance.PlayUIClick();
		} 

		private void GameManagerOnonStateChange(GameState state)
		{
			switch (state)
			{
				case GameState.Dead:
					ShowUI(3f);
					GameManager.ChangeState(GameState.GameOver);
					break;
				case GameState.GameOver:
					ShowUI(3f);
					break;
				default:
					HideUI(0);
					break;
			}
		}
	}
}