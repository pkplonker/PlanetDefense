using UnityEngine;

namespace UI
{
	public class MainMenu : UICanvas
	{
		public static void NewGame()
		{
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.NewGame);
		}

		public static void Settings()
		{
			SFXController.instance.PlayUIClick();
			Debug.Log("Settings");
		}

		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Menu) Show(2f);
			else Hide(0f);
		}

		public static void Exit()
		{
			SFXController.instance.PlayUIClick();
			Debug.Log("Exit requested");
#if UNITY_EDITOR
			UnityEditor.EditorApplication.isPlaying = false;
#endif
			Application.Quit();
		}
	}
}