using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	[SerializeField] private WaveContainer waveContainer;
	private int currentMobIndex;
	[SerializeField] private EnemySpawner enemySpawner;
	Coroutine waveCoroutine;

	private void OnEnable()
	{
		GameManager.onWaveStart += OnWaveStart;
		GameManager.onStateChange += OnStateChange;
		waveCoroutine = null;
	}

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
		currentMobIndex = 0;
		while (currentMobIndex != waveContainer.waves[waveIndex].GetSpawnLength())
		{
			yield return new WaitForSeconds(waveContainer.waves[waveIndex].spawns[currentMobIndex].nextMobDelay);
			enemySpawner.SpawnEnemy(waveContainer.waves[waveIndex].spawns[currentMobIndex].enemy);
			currentMobIndex++;
		}
	}
}