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
		[SerializeField] private float messageSpeedMin;
		[SerializeField] private float messageSpeedMax;
		[SerializeField] private Button inc;
		[SerializeField] private Button dec;

		[SerializeField] private Button continueButton;
		private List<StoryMessageUI> activeMessages = new List<StoryMessageUI>();
		[SerializeField] private LevelMessageContainer levelMessageContainer;
		[SerializeField] private AudioClip notificationSound;
		private bool newGame;

		private void Awake() => HideUI();

		private void SetContinueButton(bool show)
		{
			continueButton.gameObject.SetActive(show);
			inc.gameObject.SetActive(show);
			dec.gameObject.SetActive(show);
		}

		private Coroutine cor;
		[SerializeField] private string playerName;


		public void PlayInitialStory()
		{
			ShowUI();
			cor = StartCoroutine(DisplayMessages(levelMessageContainer.pregameLevelMessageData));
		}

		public void PlayLevelStory(int level)
		{
			if (!CheckForStoryThisLevel(level)) GameManager.Instance.ChangeState(GameState.Shop);
		}

		private bool CheckForStoryThisLevel(int level)
		{
			foreach (var lmd in levelMessageContainer.levelMessageData.Where(
				         lmd => lmd.level == level))
			{
				ShowUI();
				cor = StartCoroutine(DisplayMessages(lmd));
				return true;
			}

			return false;
		}


		private void NewMessage(MessageData md)
		{
			var sm = Instantiate(storyMessagePrefab, messageContainer);
			activeMessages.Add(sm);
			sm.Init(md.sender, md.message, playerName);
			StartCoroutine(PushToBottom());
			SFXController.instance.Playclip(notificationSound, SFXController.SFXType.SFX);
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
				var speed = PlayerPrefs.GetFloat("StorySpeed");
				Debug.Log("Speed = " + speed);
				while (timer < speed)
				{
					timer += Time.deltaTime;
					yield return null;
				}

				NewMessage(lmd.messageData[count]);
				count++;
			}

			SetContinueButton(true);
			cor = null;
		}


		public void Skip()
		{
			HideUI();
			DestroyAllMessages();
			GameManager.Instance.ChangeState(newGame ? GameState.NewWave : GameState.Shop);
			newGame = false;
		}

		private void DestroyAllMessages()
		{
			if (cor != null) StopCoroutine(cor);
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

		public void Close()
		{
			DestroyAllMessages();
			HideUI();
		}
	}
}