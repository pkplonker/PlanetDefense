using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, iDamageable, iHealable
{
	[SerializeField] private Stats stats;

	public event Action<float> onHealthChanged;
	public event Action onDeath;
	protected float currentHealth;
	public float GetMaxHealth() => stats.maxHealth;

	protected virtual void Awake()
	{
	}

	protected virtual void Start()
	{
		currentHealth = stats.maxHealth;
		onHealthChanged?.Invoke(currentHealth);
	}

	public virtual void TakeDamage(float amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Die();
		}

		onHealthChanged?.Invoke(currentHealth);
	}

	public virtual void Die()
	{
		onDeath?.Invoke();
		Debug.Log(stats.characterName + " died");
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
}