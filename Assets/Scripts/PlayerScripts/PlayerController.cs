using System;
using Interfaces;
using UnityEngine;
using Upgrades;

namespace PlayerScripts
{
	public class PlayerController : MonoBehaviour, IDamageable, IHealable, IGetStats, ICheckAlive
	{
		[SerializeField] private PlayerStats stats;
		private bool isDead;
		private float currentHealth;
		public event Action<float, float> onHealthChanged;
		public event Action<float, float> onShieldChanged;
		public event Action<PlayerController> onDeath;

		public float GetMaxHealth() => stats.GetMaxHealth();
		public float GetMaxShield() => stats.GetMaxShield();

		private SpriteRenderer spriteRenderer;
		private float currentShield;

		private void Awake()
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			spriteRenderer.enabled = false;
		}

		private void Start() => SetInitialHealth();
		public Stats GetStats() => stats;
		public bool GetIsDead() => isDead;
		public bool IsShieldUnlocked() => stats.GetIsShieldUnlocked();

		private void OnEnable()
		{
			GameManager.onStateChange += HandleGameStateChange;
			stats.health.OnValueChanged += MaxHealthChanged;
			stats.shield.OnValueChanged += MaxShieldChanged;
			stats.shieldUnlock.OnPurchase += ShieldUnlocked;
		}

		private void ShieldUnlocked()
		{
			currentShield = stats.GetMaxShield();
			onShieldChanged?.Invoke(currentShield, GetMaxShield());
		}

		private void OnDisable()
		{
			GameManager.onStateChange -= HandleGameStateChange;
			stats.health.OnValueChanged -= MaxHealthChanged;
			stats.shield.OnValueChanged -= MaxShieldChanged;
			stats.shieldUnlock.OnPurchase += ShieldUnlocked;
		}

		private void MaxShieldChanged(float before, float after)
		{
			currentShield += after - before;
			onShieldChanged?.Invoke(currentShield, GetMaxShield());
		}

		private void MaxHealthChanged(float before, float after)
		{
			currentHealth += after - before;
			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
		}

		private void HandleGameStateChange(GameState state)
		{
			if (state != GameState.NewGame)
			{
				onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
				onShieldChanged?.Invoke(currentShield, GetMaxShield());
				return;
			}
			SetInitialHealth();
			spriteRenderer.enabled = true;
		}

		private void SetInitialHealth()
		{
			currentHealth = stats.GetMaxHealth();
			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
			isDead = false;
			if (!stats.GetIsShieldUnlocked()) return;
			currentShield = stats.GetMaxShield();
			onShieldChanged?.Invoke(currentShield, GetMaxShield());
		}

		public void TakeDamage(float amount)
		{
			if (!stats.GetIsShieldUnlocked()) TakeHealthDamage(amount);
			else
			{
				if (currentShield > amount)
				{
					currentShield -= amount;
					onShieldChanged?.Invoke(currentShield, GetMaxShield());
				}
				else
				{
					amount -= currentShield;
					currentShield = 0;
					onShieldChanged?.Invoke(currentShield, GetMaxShield());
					TakeHealthDamage(amount);
				}
			}
		}

		private void TakeHealthDamage(float amount)
		{
			currentHealth -= amount;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}
			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
		}

		public void Die()
		{
			if (isDead) return;
			isDead = true;
			Debug.Log(stats.characterName + " died");
			onDeath?.Invoke(this);
			GameManager.Instance.ChangeState(GameState.Dead);
		}

		public void Heal(float amount)
		{
			currentHealth += amount;
			if (currentHealth > stats.GetMaxHealth()) currentHealth = stats.GetMaxHealth();
			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
		}
	}
}