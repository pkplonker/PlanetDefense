using System;
using UnityEngine;

namespace UI
{
    public class GameOverUI : MonoBehaviour
    {
      [SerializeField] private  CanvasGroup canvasGroup;

    

        private void OnEnable()
        {
            GameManager.onStateChange += GameManagerOnonStateChange;
            Hide();
        }

      

        private void OnDisable()
        {
            GameManager.onStateChange -= GameManagerOnonStateChange;
        }
        private void GameManagerOnonStateChange(GameState state)
        {
            if (state == GameState.Dead)
            {
                Show();
                GameManager.ChangeState(GameState.GameOver);
            }
            else if(state ==GameState.GameOver)
            {
                
            }
            else
            {
                Hide();
            }
          
        }

        private void Show()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        private void Hide()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        public void Restart()
        {
            GameManager.ChangeState(GameState.NewGame);
        }
        public void Menu()
        {
            GameManager.ChangeState(GameState.Menu);
        }
    }
}
