using System;
using UnityEngine;

namespace UI
{
	public class ShopUI : UICanvas
	{
		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;
		private void Start() => Hide();
		public static void NextLevel()
		{ 
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.NewWave);
		} 

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Shop) Show();
			else Hide();
		}
	}
}