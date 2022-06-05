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
        }

      

        private void OnDisable()
        {
            GameManager.onStateChange -= GameManagerOnonStateChange;
        }
        private void GameManagerOnonStateChange(GameState state)
        {
            if (state == GameState.Dead)
            {
                canvasGroup.alpha = 1f;
                canvasGroup.interactable = true;
                canvasGroup.blocksRaycasts = true;
                GameManager.ChangeState(GameState.GameOver);
            }
            else if(state ==GameState.GameOver)
            {
                
            }
            else
            {
                canvasGroup.alpha = 0f;
                canvasGroup.interactable = false;
                canvasGroup.blocksRaycasts = false;
            }
          
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
