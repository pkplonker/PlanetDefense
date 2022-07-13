using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using StuartHeathTools;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class StoryUI : CanvasGroupBase
	{
		[SerializeField] private Transform messageContainer;
		[SerializeField] private StoryMessageUI storyMessagePrefab;
		[SerializeField] private ScrollRect scrollRect;
		[SerializeField] private float messageSpeed;
		[SerializeField] private Button continueButton;
		private List<StoryMessageUI> activeMessages = new List<StoryMessageUI>();
		[SerializeField] private LevelMessageContainer levelMessageContainer;
		private bool newGame;
		private void OnEnable() => GameManager.onStateChange += StateChange;
		private void OnDisable() => GameManager.onStateChange -= StateChange;
		private void Awake() => HideUI();
		private void SetContinueButton(bool show) => continueButton.gameObject.SetActive(show);


		private void StateChange(GameState state)
		{
			if (state == GameState.Story)
			{
				if(!CheckForStoryThisLevel()) GameManager.Instance.ChangeState(GameState.Shop);
			}
			else if (state == GameState.NewGame)
			{
				ShowUI();
				StartCoroutine(DisplayMessages(levelMessageContainer.pregameLevelMessageData));
			}
		}

		private bool CheckForStoryThisLevel()
		{
			foreach (var lmd in levelMessageContainer.levelMessageData.Where(lmd => lmd.level == GameManager.currentWave))
			{
				ShowUI();
				StartCoroutine(DisplayMessages(lmd));
				return true;
			}

			return false;
		}


		public void NewMessage(MessageData md)
		{
			StoryMessageUI sm = Instantiate(storyMessagePrefab, messageContainer);
			activeMessages.Add(sm);
			sm.Init(md.sender, md.message);
			StartCoroutine(PushToBottom());
		}

		private IEnumerator PushToBottom()
		{
			yield return new WaitForEndOfFrame();
			scrollRect.verticalNormalizedPosition = 0;
			LayoutRebuilder.ForceRebuildLayoutImmediate((RectTransform) messageContainer.transform);
		}

		private IEnumerator DisplayMessages(LevelMessageData lmd)
		{
			if (lmd == levelMessageContainer.pregameLevelMessageData) newGame = true;
			else newGame = false;
			int count = 0;
			if (lmd.messageData == null || lmd.messageData.Count == 0) yield break;
			NewMessage(lmd.messageData[count]);
			count++;
			while (count < lmd.messageData.Count)
			{
				float timer = 0;
				while (timer < messageSpeed)
				{
					timer += Time.deltaTime;
					yield return null;
				}

				NewMessage(lmd.messageData[count]);
				count++;
			}
			SetContinueButton(true);
		}


		public void Skip()
		{
			HideUI();
			DestroyAllMessages();
			if (newGame)
			{
				GameManager.Instance.ChangeState(GameState.NewWave);

			}
			else
			{
				GameManager.Instance.ChangeState(GameState.Shop);

			}
		}

		private void DestroyAllMessages()
		{
			foreach (var mess in activeMessages)
			{
				Destroy(mess.gameObject);
			}

			activeMessages = new List<StoryMessageUI>();
		}


		protected override void ShowUI(float fadeTime = 0)
		{
			SetContinueButton(false);
			base.ShowUI(fadeTime);
		}
	}
}