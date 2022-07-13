using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Upgrades;

namespace PlayerScripts
{
	public class PlayerCombatManager : MonoBehaviour, IRegisterDestroy
	{
		[SerializeField] private Projectile projectilePrefab;
		[SerializeField] private StatBasedProjectileData playerAutoProjectileData;
		[SerializeField] private Unlockable autoShootUnlock;
		private List<Projectile> projectiles = new List<Projectile>();
		private float lastShotTime;
		[SerializeField] private Enemies.EnemySpawner enemySpawner;
		public static event Action<float, float> OnACoolDownUpdate; 
		private void OnEnable() => GameManager.onStateChange += OnStateChange;
		private void OnDisable() => GameManager.onStateChange -= OnStateChange;

		private void OnStateChange(GameState state)
		{
			if (state !=GameState.InGame) DestroyAllProjectiles();
		}

		private void DestroyAllProjectiles()
		{
			for (var i = 0; i < projectiles.Count; i++)
			{
				if (projectiles[i] != null) Destroy(projectiles[i]);
			}
		}


		private void Start()=>DestroyAllProjectiles();
		

		private void Update()
		{
			if (GameManager.GetCurrentState() != GameState.InGame) return;
			if (!autoShootUnlock.GetIsUnlocked()) return;
			OnACoolDownUpdate?.Invoke(playerAutoProjectileData.GetCooldown()-(Time.time - lastShotTime), playerAutoProjectileData.GetCooldown());
			if (!(lastShotTime + playerAutoProjectileData.GetCooldown() < Time.time)) return;
			var target = AcquireTarget(playerAutoProjectileData);
			if (target != null)
			{
				lastShotTime = Time.time;

				projectiles.Add(Shoot(transform, target.transform));
			}
		}

		public void Shoot(Vector3 direction, ProjectileData projectileData) =>
			projectiles.Add(CreateProjectileWithDirection(direction, projectileData));


		private Projectile Shoot(Transform shooter, Transform targetTransform, ProjectileData projectileData = null)
		{
			if (projectileData == null) projectileData = playerAutoProjectileData;
			if (shooter == null || targetTransform == null) return CreateProjectile(targetTransform, projectileData);
			return targetTransform.GetComponent<ICheckAlive>().GetIsDead()
				? null
				: CreateProjectile(targetTransform, projectileData);
		}

		private Projectile CreateProjectile(Transform targetTransform, ProjectileData projectileData)
		{
			var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
			Transform projectileTransform;
			(projectileTransform = projectile.transform).localEulerAngles =
				-Quaternion.LookRotation(targetTransform.position - transform.position).eulerAngles;
			projectileTransform.localScale = Vector3.one;
			projectile.Init(this, projectileData, Stats.Team.Enemy, targetTransform);
			return projectile;
		}

		private Projectile CreateProjectileWithDirection(Vector3 direction, ProjectileData projectileData)
		{
			var projectile = Instantiate(projectilePrefab).GetComponent<Projectile>();
			Transform projectileTransform;
			(projectileTransform = projectile.transform).localEulerAngles =
				-Quaternion.LookRotation(direction - transform.position).eulerAngles;
			projectileTransform.localScale = Vector3.one;
			projectile.Init(this, projectileData, Stats.Team.Enemy, direction);
			return projectile;
		}

		private Enemies.Enemy AcquireTarget(ProjectileData playerProjectileData)
		{
			var count = enemySpawner.spawnedEnemies.Count;
			return count switch
			{
				0 => null,
				_ => ClosestEnemy(playerProjectileData)
			};
		}

		private Enemies.Enemy ClosestEnemy(ProjectileData playerProjectileData)
		{
			var closestDistance = float.MaxValue;
			Enemies.Enemy closestEnemy = null;
			foreach (var enemy in enemySpawner.spawnedEnemies)
			{
				var d = Vector2.Distance(transform.position, enemy.transform.position);
				if (!(d < closestDistance)) continue;
				closestDistance = d;
				closestEnemy = enemy;
			}

			return playerProjectileData.GetRange() > closestDistance ? closestEnemy : null;
		}

		public void RegisterDestroy(object obj)
		{
			if (obj is Projectile projectile) projectiles.Remove(projectile);
		}
	}
}