using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using PlayerScripts;
using UnityEngine;
using Upgrades;

namespace Enemies
{
	public class Enemy : MonoBehaviour, IDamageable, IHealable, IGetStats, IDestroyable, ICheckAlive, IRegisterDestroy
	{
		private PlayerController target;
		private EnemyStats stats;
		private float currentHealth;
		public event Action<float> onHealthChanged;
		public event Action<Enemy> onDeath;
		public float GetMaxHealth() => stats.maxHealth;
		private bool inRange = false;
		[SerializeField] private Projectile projectilePrefab;
		private float lastShotTime = 0;
		private bool isDead = false;
		private List<Projectile> projectiles = new List<Projectile>();
		public Stats GetStats() => stats;
		public bool GetIsDead() => isDead;

		public void Init(PlayerController target, EnemyStats stats)
		{
			this.stats = stats;
			this.target = target;
			currentHealth = stats.maxHealth;
			onHealthChanged?.Invoke(currentHealth);
		}

		private void Update()
		{
			if (GameManager.GetCurrentState() == GameState.Paused) return;

			if (inRange)
			{
						if (lastShotTime + stats.projectileData.GetSpeed() < Time.time) Shoot();
			}
			else
			{
				if (Vector3.Distance(transform.position, target.transform.position) > stats.projectileData.GetRange())
				{
					transform.position =
						Vector3.MoveTowards(transform.position, target.transform.position,
							stats.movementSpeed * Time.deltaTime);
					transform.rotation =
						Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
				}
				else inRange = true;
			}
		}

		private void Shoot()
		{
			if (target.GetIsDead()) return;
			var projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
			projectile.Init(this, stats.projectileData, Stats.Team.Player, target.transform);
			projectiles.Add(projectile);
			lastShotTime = Time.time;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponent<PlayerController>();
			if (player == null) return;
			TakeDamage(stats.maxHealth);
			player.TakeDamage(stats.impactDamage);
		}

		public void TakeDamage(float amount)
		{
			currentHealth -= amount;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}

			onHealthChanged?.Invoke(currentHealth);
		}

		public void Die()
		{
			isDead = true;
			onDeath?.Invoke(this);
			Destroy(gameObject);
		}

		public void Heal(float amount)
		{
			currentHealth += amount;
			if (currentHealth > stats.maxHealth) currentHealth = stats.maxHealth;
			onHealthChanged?.Invoke(currentHealth);
		}


		public void DestroyEntity()
		{
			foreach (var p in projectiles.Where(p => p != null))
			{
				p.DestroyEntity();
			}

			Destroy(gameObject);
		}

		public void RegisterDestroy(object obj)
		{
			if (obj is Projectile projectile)
			{
				projectiles.Remove(projectile);
			}
		}
	}
}