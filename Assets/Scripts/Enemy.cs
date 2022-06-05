using System;
using System.Collections.Generic;
using System.Linq;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public class Enemy : MonoBehaviour, IDamageable, IHealable, IGetStats, IDestroyable,ICheckAlive
{
	private Player target;
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

	public void Init(Player target, EnemyStats stats)
	{
		this.stats = stats;
		this.target = target;
		currentHealth = stats.maxHealth;
		onHealthChanged?.Invoke(currentHealth);
	}

	private void Update()
	{
		if (inRange)
		{
			if (lastShotTime + stats.attackSpeed < Time.time)
			{
				Shoot();
			}
		}
		else
		{
			if (Vector3.Distance(transform.position, target.transform.position) > stats.attackRange)
			{
				transform.position =
					Vector3.MoveTowards(transform.position, target.transform.position, stats.speed * Time.deltaTime);
			}
			else
			{
				inRange = true;
			}
		}
	}

	private void Shoot()
	{
		if (target.GetIsDead()) return;
		Projectile projectile = Instantiate(projectilePrefab, transform).GetComponent<Projectile>();
		projectile.Init(stats.projectileData, Stats.Team.Player, target.transform);
		projectiles.Add(projectile);
		Debug.Log("Spawned projectile");
		lastShotTime = Time.time;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.GetComponent<Player>();
		if (player == null) return;
		TakeDamage(stats.maxHealth);
		player.TakeDamage(stats.damage);
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
		Debug.Log(stats.characterName + " died");
		onDeath?.Invoke(this);
		Destroy(gameObject);
	}

	public void Heal(float amount)
	{
		currentHealth += amount;
		if (currentHealth > stats.maxHealth)
		{
			currentHealth = stats.maxHealth;
		}

		onHealthChanged?.Invoke(currentHealth);
	}

	public Stats GetStats()
	{
		return stats;
	}

	public void DestroyEntity()
	{
		foreach (var p in projectiles.Where(p => p != null))
		{
			p.DestroyEntity();
		}
		Destroy(gameObject);
	}

	public bool GetIsDead() => isDead;
}