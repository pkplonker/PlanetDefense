using UnityEngine;

namespace UI
{
	public class MainMenu : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;


		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Menu)
			{
				ShowMenu();
			}
			else
			{
				HideMenu();
			}
		}

		private void HideMenu()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		private void ShowMenu()
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		public static void NewGame()
		{
			GameManager.ChangeState(GameState.NewGame);
		}

		public static void Settings()
		{
			Debug.Log("Settings");
		}

		public static void Exit()
		{
			Debug.Log("Exit requested");
			Application.Quit();
		}
	}
}