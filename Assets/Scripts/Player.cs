using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable,IHealable,IGetStats
{
	[SerializeField] private PlayerStats stats;
	private bool isDead = false;
	private float currentHealth;
	public event Action<float> onHealthChanged;
	public event Action<Player> onDeath;
	public float GetMaxHealth() => stats.maxHealth;

	private void Start()
	{
		currentHealth = stats.maxHealth;
		onHealthChanged?.Invoke(currentHealth);
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

	public bool GetIsDead() => isDead;
}
