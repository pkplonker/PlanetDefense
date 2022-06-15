using UnityEngine;

namespace UI
{
    public abstract class UICanvas : MonoBehaviour
    {
        [SerializeField] protected CanvasGroup canvasGroup;

        protected virtual void Show()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        protected virtual void Hide()
        {
            if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }
}
