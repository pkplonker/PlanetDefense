using TMPro;
using UnityEngine;

namespace UI
{
	public class WaveUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI waveText;
		[SerializeField] private TextMeshProUGUI thisWaveText;

		private void OnEnable()
		{
			GameManager.onWaveStart += ChangeWave;
			WaveSpawner.OnNewMobSpawned += ChangeWaveData;
		}

		private void ChangeWaveData(int spawnedMobs, int maxSpawnedMobs)
		{
			thisWaveText.text = $"Targets: {spawnedMobs+1}/{maxSpawnedMobs}";
		}

		private void OnDisable()
		{
			GameManager.onWaveStart -= ChangeWave;
			WaveSpawner.OnNewMobSpawned -= ChangeWaveData;
		}

		private void ChangeWave(int currentWave) =>
			waveText.text = "Wave: " + (currentWave + 1);
	}
}