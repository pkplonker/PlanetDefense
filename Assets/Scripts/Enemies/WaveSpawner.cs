using System;
using System.Collections;
using Enemies;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveContainer waveContainer;
	private int currentMobIndex;
	[SerializeField] private Enemies.EnemySpawner enemySpawner;
	Coroutine waveCoroutine;
	int waveIndex = 0;
	public static event Action<int, int> OnNewMobSpawned;

	private void OnEnable()
	{
		GameManager.onWaveStart += OnWaveStart;
		GameManager.onStateChange += OnStateChange;
		waveCoroutine = null;
	}

	private int GetWaveSize() => waveContainer.waves[waveIndex].GetSpawnLength();

	private void OnStateChange(GameState state)
	{
		if (waveCoroutine == null) return;
		if (state == GameState.Dead || state == GameState.Complete || state == GameState.Shop ||
		    state == GameState.WaveOver)
		{
			StopCoroutine(waveCoroutine);
		}
	}

	private void OnWaveStart(int waveIndex)
	{
		this.waveIndex = waveIndex;
		Logger.Instance.Log("Wave Start");
		if (waveCoroutine == null) waveCoroutine = StartCoroutine(WaveCoroutine(waveIndex));
		else
		{
			StopCoroutine(waveCoroutine);
			waveCoroutine = StartCoroutine(WaveCoroutine(waveIndex));
		}

		currentMobIndex = 0;
	}

	private void OnDisable()
	{
		GameManager.onWaveStart += OnWaveStart;
		if (waveCoroutine != null)
		{
			StopCoroutine(waveCoroutine);
		}
	}

	private IEnumerator WaveCoroutine(int waveIndex)
	{
		Logger.Instance.Log("Started Spawn cor");

		currentMobIndex = 0;
		OnNewMobSpawned?.Invoke(currentMobIndex, GetWaveSize());

		while (currentMobIndex != waveContainer.waves[waveIndex].GetSpawnLength())
		{
			yield return new WaitForSeconds(waveContainer.waves[waveIndex].spawns[currentMobIndex].nextMobDelay);
			enemySpawner.SpawnEnemy(waveContainer.waves[waveIndex].spawns[currentMobIndex].enemyStats);
			OnNewMobSpawned?.Invoke(currentMobIndex, GetWaveSize());
			currentMobIndex++;
		}
	}
}