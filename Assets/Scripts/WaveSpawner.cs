using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveContainer waveContainer;
	private int currentSpawnIndex;
	private Coroutine currentSpawnCor;
	[SerializeField] private EnemySpawner enemySpawner;
	[SerializeField] int currentWave = 0;
	private readonly float INITIAL_WAVE_DELAY = 1f;
	public static event Action<int, int> OnMobCountChange;
	public static event Action<int> OnCurrentWaveChange; 
	private void OnEnable()
	{
		GameManager.onStateChange += GameStateChange;
	}

	private void OnDisable()
	{
		GameManager.onStateChange -= GameStateChange;
	}

	

	private void GameStateChange(GameState state)
	{
		if (state == GameState.Paused)
		{
			PauseSpawn();
		}
		else if (state == GameState.NewGame)
		{
			StartNewGame();
		}
		else if (state == GameState.WaveOver)
		{
		}
		else if (state == GameState.NewWave)
		{
			NextWave();
		}
	}

	private void NextWave()
	{
		currentWave++;
		if (IsLastWave())
		{
			Debug.Log("Game over");
			return;
		}

		StartNewWave(currentWave);
	}

	private bool IsLastWave()
	{
		return waveContainer.IsLastWave(currentWave);
	}

	private void StartNewGame()
	{
		currentSpawnIndex = 0;
		StartNewWave(0);
	}

	private void StartNewWave(int wave)
	{
		currentWave = wave;
		currentSpawnIndex = 0;
		currentSpawnCor = StartCoroutine(SpawnCycle(INITIAL_WAVE_DELAY));
		OnCurrentWaveChange?.Invoke(currentWave);
		OnMobCountChange?.Invoke(currentSpawnIndex,waveContainer.GetWaveByIndex(currentWave).GetNumberOfSpawns());

	}

	private IEnumerator SpawnCycle(float delay)
	{
		float timer = 0f;
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
		enemySpawner.SpawnEnemy(spawn.enemy);
		currentSpawnIndex++;
		OnMobCountChange?.Invoke(currentSpawnIndex,waveContainer.GetWaveByIndex(currentWave).GetNumberOfSpawns());

		if (!waveContainer.GetWaveByIndex(currentWave).IsLastMob(currentSpawnIndex))
		{
			currentSpawnCor = StartCoroutine(SpawnCycle(spawn.nextMobDelay));
		}
		else
		{
			GameManager.ChangeState(GameState.WaveOver);
		}

		currentSpawnCor = null;
	}


	private void PauseSpawn()
	{
		//todo wave spawner - pause spawn
	}
}