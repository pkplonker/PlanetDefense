using UnityEngine;

namespace UI
{
   public class PauseMenu : MonoBehaviour
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
   }
}
