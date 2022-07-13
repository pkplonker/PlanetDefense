using StuartHeathTools;
using UnityEngine;

namespace UI
{
	public class HUDController : CanvasGroupBase
	{


		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			HideUI(0);
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state is GameState.InGame or  GameState.NewWave) ShowUI(0);
			else HideUI(0);
		}


		public void Pause()
		{
			GameManager.Instance.ChangeState(GameState.Paused);
			SFXController.instance.PlayUIClick();
		}
	}
}