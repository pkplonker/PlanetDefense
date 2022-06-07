using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Enemy enemyPrefab;
	[SerializeField] private EnemyStats enemyStats;
	private List<Enemy> spawnedEnemies = new List<Enemy>();
	private Camera cam;
	[SerializeField] private Player player;
	public static event Action<EnemyStats> OnEnemyDeath;

	private void Awake()
	{
		cam = Camera.main;
	}

	private void OnEnable()
	{
		GameManager.onStateChange += HandleGameStateChange;
	}

	private void OnDisable()
	{
		GameManager.onStateChange -= HandleGameStateChange;
	}

	private void HandleGameStateChange(GameState state)
	{
		if (state == GameState.NewGame)
		{
			foreach (var enemy in spawnedEnemies)
			{
				if (enemy != null) enemy.DestroyEntity();
			}
		}
	}

	public void SpawnEnemy(EnemyStats stats)
	{
		Vector3 spawnPos = CalculateSpawnPosition();
		Enemy enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
		enemy.Init(player, stats);
		spawnedEnemies.Add(enemy);
		enemy.onDeath += HandleEnemyDeath;
	}

	private Vector3 CalculateSpawnPosition()
	{
		float x;
		float y;
		if (Utility.RandomBool()) //spawn left or right
		{
			x = Utility.RandomBool() ? 1.1f : -0.1f;
			y = Random.value;
		}
		else // spawn top or bottom
		{
			x = Random.value;
			y = Utility.RandomBool() ? 1.1f : -0.1f;
		}

		Vector3 pos = new Vector3(x, y, 0f);
		pos = cam.ViewportToWorldPoint(pos);

		return pos;
	}

	void HandleEnemyDeath(Enemy entity)
	{
		spawnedEnemies.Remove(entity);
		entity.onDeath -= HandleEnemyDeath;
		GameManager.instance.EnemyDeath(null);
	}
}