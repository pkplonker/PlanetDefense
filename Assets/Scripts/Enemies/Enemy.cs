using System;
using System.Collections;
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
		private PlayerHealth target;
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
		[SerializeField] private GameObject explosionPrefab;

		public void Init(PlayerHealth target, EnemyStats stats)
		{
			this.stats = stats;
			this.target = target;
			currentHealth = stats.maxHealth;
			onHealthChanged?.Invoke(currentHealth);
		}

		private void Update()
		{
			if (GameManager.GetCurrentState() == GameState.Paused) return;
			if (stats.GetProjectileData() == null)
			{
				MoveTowards();
			}
			else
			{
				if (inRange)
				{
					if (lastShotTime + stats.GetProjectileData().GetSpeed() < Time.time) Shoot();
				}
				else
				{
					if (Vector3.Distance(transform.position, target.transform.position) > stats.GetProjectileData().GetRange())
					{
						MoveTowards();
					}
					else inRange = true;
				}
			}
			
		}

		private void MoveTowards()
		{
			transform.position =
				Vector3.MoveTowards(transform.position, target.transform.position,
					stats.movementSpeed * Time.deltaTime);
			transform.rotation =
				Quaternion.LookRotation(Vector3.forward, target.transform.position - transform.position);
		}

		private void Shoot()
		{
			if (target.GetIsDead()) return;
			var projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
			projectile.Init(this, stats.GetProjectileData(), Stats.Team.Player, target.transform);
			projectiles.Add(projectile);
			lastShotTime = Time.time;
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			var player = other.GetComponent<PlayerHealth>();
			if (player == null) return;
			TakeDamage(stats.maxHealth,other.GetComponent<Transform>().position);
			player.TakeDamage(stats.impactDamage, other.GetComponent<Transform>().position);
		}

		public void TakeDamage(float amount, Vector3 hitPoint)
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
			Explode();
		}

		private void Explode()
		{
			Instantiate(explosionPrefab,transform.position,Quaternion.identity);
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
			for (var i = projectiles.Count - 1; i >= 0; i--)
			{
				if(projectiles[i]!=null) projectiles[i].DestroyEntity();

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

		public EnemyStats GetEnemyStats() => stats;
	}
}