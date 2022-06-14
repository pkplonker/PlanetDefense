using UnityEngine;

namespace UI
{
	public class HUDController : UICanvas
	{


		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			Hide();
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.InGame) Show();
			else Hide();
		}


		public void Pause()
		{
			GameManager.ChangeState(GameState.Paused);
			SFXController.instance.PlayUIClick();
		}
	}
}