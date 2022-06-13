using UnityEngine;

namespace UI
{
	public class PauseMenu : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;

		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			HidePause();
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Paused)
			{
				ShowPause();
			}
			else
			{
				HidePause();
			}
		}

		private void HidePause()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		private void ShowPause()
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		public void Resume()
		{
			GameManager.ChangeState(GameState.InGame);
		}

		public void Restart()
		{
			MainMenu.NewGame();
		}

		public void Giveup()
		{
			GameManager.ChangeState(GameState.Menu);
		}

		public void Settings()
		{
			MainMenu.Settings();
		}
	}
}