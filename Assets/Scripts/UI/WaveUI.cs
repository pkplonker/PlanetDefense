using TMPro;
using UnityEngine;

namespace UI
{
    public class WaveUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI waveText;
        [SerializeField] private TextMeshProUGUI mobText;

        private void OnEnable()
        {
            GameManager.onWaveStart += ChangeWave;
        }
    
        private void OnDisable()
        {
            GameManager.onWaveStart -= ChangeWave;
        }

        private void ChangeWave(int currentWave)
        {
            waveText.text = "Wave: " + (currentWave+1) + " ("+currentWave+")";
        }
    }
}
