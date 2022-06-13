using System;
using System.Collections.Generic;
using Abilities;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
	private PlayerStats stats;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private PlayerProjectileData attack;
	[SerializeField] private List<Ability> abilities;
	private List<Ability> useableAbilities;
	private List<Projectile> projectiles = new List<Projectile>();
	private float lastShotTime;
	[SerializeField] private EnemySpawner enemySpawner;

	private void Awake()
	{
		stats = (PlayerStats) GetComponent<Player>().GetStats();
	}

	private void OnEnable()
	{
		GameManager.onStateChange += OnStateChange;
	}

	private void OnStateChange(GameState state)
	{
		if (state == GameState.NewGame || state == GameState.GameOver)
		{
			DestroyAllProjectiles();
		}
	}

	private void DestroyAllProjectiles()
	{
		foreach (var p in projectiles)
		{
			if (p == null) continue;
			p.DestroyEntity();
		}
	}

	private void OnDisable()
	{
		GameManager.onStateChange -= OnStateChange;
	}

	private void Start()
	{
		foreach (var a in abilities)
		{
			useableAbilities.Add(Instantiate(a));
		}
	}

	private void Update()
	{
		if (GameManager.GetCurrentState() != GameState.InGame) return;
		if (!(lastShotTime + attack.attackCooldown < Time.time)) return;
		var target = AcquireTarget(attack);
		if (target != null)
		{
			projectiles.Add(Shoot(transform, Stats.Team.Enemy, target.transform));
		}
	}

	private Projectile Shoot(Transform shooter, Stats.Team targetTeam, Transform targetTransform)
	{
		if (shooter == null || targetTransform == null)
		{
			Debug.LogWarning("Missing info");
			return null;
		}

		if (targetTransform.GetComponent<ICheckAlive>().GetIsDead()) return null;
		Projectile projectile = Instantiate(projectilePrefab, shooter).GetComponent<Projectile>();
		projectile.transform.localScale= Vector3.one;
		projectile.Init(attack, Stats.Team.Enemy, targetTransform);
		lastShotTime = Time.time;
		return projectile;
	}

	private Enemy AcquireTarget(PlayerProjectileData playerProjectileData)
	{
		int count = enemySpawner.spawnedEnemies.Count;
		return count switch
		{
			0 => null,
			_ => ClosestEnemy(playerProjectileData)
		};
	}

	private Enemy ClosestEnemy(PlayerProjectileData playerProjectileData)
	{
		float closestDistance = float.MaxValue;
		Enemy closestEnemy = null;
		foreach (var enemy in enemySpawner.spawnedEnemies)
		{
			float d = Vector2.Distance(transform.position, enemy.transform.position);
			if (d < closestDistance)
			{
				closestDistance = d;
				closestEnemy = enemy;
			}
		}

		return playerProjectileData.range > closestDistance ? closestEnemy : null;
	}

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position, 6);
	}
}