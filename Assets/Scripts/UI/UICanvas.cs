using System.Collections;
using UnityEngine;

namespace UI
{
	public abstract class UICanvas : MonoBehaviour
	{
		[SerializeField] protected CanvasGroup canvasGroup;

		protected virtual void Show(float fadeTime = 0.33f)
		{
			if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
			if (fadeTime > 0) StartCoroutine(FadeOverTime(0, 1, fadeTime));
			else canvasGroup.alpha = 1f;
			canvasGroup.interactable = true;
			canvasGroup.blocksRaycasts = true;
		}

		protected virtual void Hide(float fadeTime = 0.33f)
		{
			if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
			if (fadeTime > 0) StartCoroutine(FadeOverTime(1, 0, fadeTime));
			else canvasGroup.alpha = 0f;
			canvasGroup.interactable = false;
			canvasGroup.blocksRaycasts = false;
		}

		IEnumerator FadeOverTime(float start, float end, float duration)
		{
			for (var t = 0f; t < duration; t += Time.deltaTime)
			{
				var normalizedTime = t / duration;
				canvasGroup.alpha = Mathf.Lerp(start, end, normalizedTime);
				

				yield return null;
			}

			canvasGroup.alpha = end;
		}
	}
}