using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable, IHealable, IGetStats, ICheckAlive
{
	[SerializeField] private PlayerStats stats;
	private bool isDead = false;
	private float currentHealth;
	public event Action<float, float> onHealthChanged;
	public event Action<Player> onDeath;
	public float GetMaxHealth() => stats.maxHealth;
	private SpriteRenderer spriteRenderer;

	private void Awake()
	{
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		spriteRenderer.enabled = false;
		
	}
	private void Start() => SetInitialHealth();
	private void OnEnable() => GameManager.onStateChange += HandleGameStateChange;
	private void OnDisable() => GameManager.onStateChange -= HandleGameStateChange;
	public Stats GetStats() => stats;
	public bool GetIsDead() => isDead;

	private void HandleGameStateChange(GameState state)
	{
		if (state != GameState.NewGame) return;
		SetInitialHealth();
		spriteRenderer.enabled = true;
	}

	private void SetInitialHealth()
	{
		currentHealth = stats.maxHealth;
		onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
		isDead = false;
	}

	public void TakeDamage(float amount)
	{
		currentHealth -= amount;
		if (currentHealth <= 0)
		{
			currentHealth = 0;
			Die();
		}
//
		onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
	}

	public void Die()
	{
		if (isDead) return;
		isDead = true;
		Debug.Log(stats.characterName + " died");
		onDeath?.Invoke(this);
		GameManager.ChangeState(GameState.Dead);
	}

	public void Heal(float amount)
	{
		currentHealth += amount;
		if (currentHealth > stats.maxHealth)
		{
			currentHealth = stats.maxHealth;
		}

		onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
	}
}