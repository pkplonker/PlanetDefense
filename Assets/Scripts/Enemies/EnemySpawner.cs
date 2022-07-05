using System;
using System.Collections.Generic;
using System.Linq;
using PlayerScripts;
using StuartHeathTools;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies
{
	public class EnemySpawner : MonoBehaviour
	{
		[SerializeField] private Enemy enemyPrefab;
		[SerializeField] private EnemyStats enemyStats;
		public List<Enemy> spawnedEnemies { get; private set; } = new List<Enemies.Enemy>();
		private Camera cam;
		[SerializeField] private PlayerController player;
		public static event Action<EnemyStats> OnEnemyDeath;

		private void Awake() => cam = Camera.main;
		private void OnEnable() => GameManager.onStateChange += HandleGameStateChange;
		private void OnDisable() => GameManager.onStateChange -= HandleGameStateChange;

		private void HandleGameStateChange(GameState state)
		{
			if (state is GameState.NewGame or GameState.NewWave or GameState.Complete or GameState.GameOver or GameState
				.Menu)
			{
				DestroyOldEnemies();
			}

			spawnedEnemies = new List<Enemies.Enemy>();
		}

		private void DestroyOldEnemies()
		{
			foreach (var enemy in spawnedEnemies.Where(enemy => enemy != null))
			{
				enemy.DestroyEntity();
			}

			foreach (var x in GetComponentsInChildren<Transform>())
			{
				if (x == transform) continue;
				Destroy(x.gameObject);
			}
		}

		public void SpawnEnemy(EnemyStats stats)
		{
			var spawnPos = CalculateSpawnPosition();
			var enemy = Instantiate(enemyPrefab, spawnPos, Quaternion.identity, transform);
			enemy.Init(player, stats);
			spawnedEnemies.Add(enemy);
			enemy.GetComponent<SpriteRenderer>().sprite = stats.GetSprite();
			enemy.onDeath += HandleEnemyDeath;
		}

		private Vector3 CalculateSpawnPosition()
		{
			float x;
			float y;
			if (UtilityRandom.RandomBool()) //spawn left or right
			{
				x = UtilityRandom.RandomBool() ? 1.1f : -0.1f;
				y = Random.value;
			}
			else // spawn top or bottom
			{
				x = Random.value;
				y = UtilityRandom.RandomBool() ? 1.1f : -0.1f;
			}

			Vector3 pos = new Vector3(x, y, 0f);
			pos = cam.ViewportToWorldPoint(pos);
			pos.z = 0;
			return pos;
		}

		private void HandleEnemyDeath(Enemies.Enemy entity)
		{
			spawnedEnemies.Remove(entity);
			entity.onDeath -= HandleEnemyDeath;
			GameManager.instance.EnemyDeath(null);
			OnEnemyDeath?.Invoke(enemyStats);
		}
	}
}