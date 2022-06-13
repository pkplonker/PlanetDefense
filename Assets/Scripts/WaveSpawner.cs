using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveContainer waveContainer;
	private int currentSpawnIndex;
	[SerializeField] private EnemySpawner enemySpawner;
	private readonly float INITIAL_WAVE_DELAY = 1f;
	public static event Action<int, int> OnMobCountChange;
	private int kills;

	private void OnEnable()
	{
		GameManager.onWaveStart += StartNewWave;
	}


	private void OnDisable()
	{
		GameManager.onWaveStart -= StartNewWave;
	}


	private void StartNewWave(int wave)
	{
		currentSpawnIndex = 0;
		StartCoroutine(SpawnCycle(INITIAL_WAVE_DELAY, wave));
		OnMobCountChange?.Invoke(currentSpawnIndex, waveContainer.GetWaveByIndex(wave).GetNumberOfSpawns());
	}

	private IEnumerator SpawnCycle(float delay, int currentWave)
	{
		var timer = 0f;
		while (timer < delay)
		{
			while (GameManager.GetCurrentState() == GameState.Paused)
			{
				yield return null;
			}
			timer += Time.deltaTime;
			yield return null;
		}

		Spawn spawn = waveContainer.GetWaveByIndex(currentWave).GetSpawnByIndex(currentSpawnIndex);
		if (spawn != null)
		{
			enemySpawner.SpawnEnemy(spawn.enemy);
			currentSpawnIndex++;
			OnMobCountChange?.Invoke(currentSpawnIndex, waveContainer.GetWaveByIndex(currentWave).GetNumberOfSpawns());
		}
		if (!waveContainer.GetWaveByIndex(currentWave).IsLastMob(currentSpawnIndex))
		{
			StartCoroutine(SpawnCycle(spawn.nextMobDelay, currentWave));
		}
	}
}