using UnityEngine;

namespace UI
{
    public class ShopUI : MonoBehaviour
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
            if (state == GameState.Shop)
            {
                OpenShop();
            }
            else
            {
                CloseShop();
            }
        }

       
        private void CloseShop()
        {
            canvasGroup.alpha = 0f;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }

        private void OpenShop()
        {
            canvasGroup.alpha = 1f;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }

        public static void NextLevel()
        {
            GameManager.ChangeState(GameState.NewWave);
        }
    }
}
