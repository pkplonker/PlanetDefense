using UnityEngine;

namespace UI
{
	public class HUDController : MonoBehaviour
	{
		[SerializeField] private CanvasGroup canvasGroup;


		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			HideHud();
		}


		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.InGame)
			{
				ShowHUD();
			}
			else
			{
				HideHud();
			}
		}

		private void HideHud()
		{
			canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		private void ShowHUD()
		{
			canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		public void Pause()
		{
			GameManager.ChangeState(GameState.Paused);
		}
	}
}