using StuartHeathTools;
using TMPro;
using UnityEngine;

namespace UI
{
	public class GameOverUI : CanvasGroupBase
	{
		[SerializeField] private TextMeshProUGUI mainText;
		[SerializeField] private TextMeshProUGUI subtitleText;
		[SerializeField] private string titleWin;
		[SerializeField] private string titleLose;
		[SerializeField] private string subtitleWin;
		[SerializeField] private string subtitleLose;

		private void Start() => HideUI(0f);
		private void OnEnable() => GameManager.onStateChange += GameManagerOnonStateChange;
		private void OnDisable() => GameManager.onStateChange -= GameManagerOnonStateChange;

		public void Restart()
		{
			GameManager.Instance.ChangeState(GameState.NewGame);
			HideUI(0.3f);
			SFXController.instance.PlayUIClick();
		}

		public void Menu()
		{
			GameManager.Instance.ChangeState(GameState.Menu);
			HideUI(0.3f);
			SFXController.instance.PlayUIClick();
		}

		private void GameManagerOnonStateChange(GameState state)
		{
			switch (state)
			{
				case GameState.Dead:
					ShowDead();
				//	GameManager.Instance.ChangeState(GameState.GameOver);
					break;
				case GameState.Complete:
					ShowWin();
					break;
				
			}
		}

		private void ShowWin()
		{
			Logger.Log("Requesting show ui Win");

			ShowUI(2f);
			mainText.text = titleWin;
			subtitleText.text = subtitleWin;
		}

		private void ShowDead()
		{
			Logger.Log("Requesting show loss");
			ShowUI(2f);
			mainText.text = titleLose;
			subtitleText.text = subtitleLose;
			GameManager.Instance.ChangeState(GameState.GameOver);
		}
	}
}