using UnityEngine;

namespace UI
{
	public class HUDController : UICanvas
	{


		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			Hide(0);
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.InGame) Show(0);
			else Hide(0);
		}


		public void Pause()
		{
			GameManager.ChangeState(GameState.Paused);
			SFXController.instance.PlayUIClick();
		}
	}
}