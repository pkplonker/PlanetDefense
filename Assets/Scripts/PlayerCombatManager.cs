using System;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
	private PlayerStats stats;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private PlayerProjectileData playerProjectileData;
	[SerializeField] private List<Ability> abilities;
	private List<Ability> useableAbilities;
	private List<Projectile> projectiles = new List<Projectile>();
	private float lastShotTime;
	[SerializeField] private EnemySpawner enemySpawner;
	private void Awake() => stats = (PlayerStats) GetComponent<Player>().GetStats();
	private void OnEnable() => GameManager.onStateChange += OnStateChange;
	private void OnDisable() => GameManager.onStateChange -= OnStateChange;

	private void OnStateChange(GameState state)
	{
		if (state is GameState.NewGame or GameState.GameOver)
		{
			DestroyAllProjectiles();
		}
	}

	private void DestroyAllProjectiles()
	{
		foreach (var p in projectiles.Where(p => p != null))
		{
			p.DestroyEntity();
		}
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
		if (!(lastShotTime + playerProjectileData.attackCooldown < Time.time)) return;
		var target = AcquireTarget(playerProjectileData);
		if (target != null)
		{
			projectiles.Add(Shoot(transform, target.transform));
		}
	}

	public void Shoot(Vector3 direction, ProjectileData projectileData)
	{
		projectiles.Add(CreateProjectileWithDirection(direction, projectileData));
	}

	private Projectile Shoot(Transform shooter, Transform targetTransform, ProjectileData projectileData = null)
	{
		if (projectileData == null) projectileData = playerProjectileData;
		if (shooter != null && targetTransform != null)
		{
			if (targetTransform.GetComponent<ICheckAlive>().GetIsDead())
				return null;
		}

		return CreateProjectile(targetTransform, projectileData);
	}

	private Projectile CreateProjectile(Transform targetTransform, ProjectileData projectileData)
	{
		var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
		projectile.transform.localEulerAngles =
			-Quaternion.LookRotation(targetTransform.position - transform.position).eulerAngles;
		projectile.transform.localScale = Vector3.one;
		projectile.Init(projectileData, Stats.Team.Enemy, targetTransform);
		lastShotTime = Time.time;
		return projectile;
	}

	private Projectile CreateProjectileWithDirection(Vector3 direction, ProjectileData projectileData)
	{
		var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
		projectile.transform.localEulerAngles =
			-Quaternion.LookRotation(direction - transform.position).eulerAngles;
		projectile.transform.localScale = Vector3.one;
		projectile.Init(projectileData, Stats.Team.Enemy, direction);
		lastShotTime = Time.time;
		return projectile;
	}

	private Enemy AcquireTarget(PlayerProjectileData playerProjectileData)
	{
		var count = enemySpawner.spawnedEnemies.Count;
		return count switch
		{
			0 => null,
			_ => ClosestEnemy(playerProjectileData)
		};
	}

	private Enemy ClosestEnemy(PlayerProjectileData playerProjectileData)
	{
		var closestDistance = float.MaxValue;
		Enemy closestEnemy = null;
		foreach (var enemy in enemySpawner.spawnedEnemies)
		{
			var d = Vector2.Distance(transform.position, enemy.transform.position);
			if (!(d < closestDistance)) continue;
			closestDistance = d;
			closestEnemy = enemy;
		}

		return playerProjectileData.range > closestDistance ? closestEnemy : null;
	}
}