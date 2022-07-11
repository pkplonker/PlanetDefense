using System;
using Interfaces;
using UnityEngine;
using Upgrades;

namespace PlayerScripts
{
	public class PlayerHealth : MonoBehaviour, IDamageable, IHealable, IGetStats, ICheckAlive
	{
		[SerializeField] private PlayerStats stats;
		private bool isDead;
		private float currentHealth;
		public event Action<float, float> onHealthChanged;
		public event Action onTakeDamage;

		public event Action<float, float> onShieldChanged;
		public event Action<PlayerHealth> onDeath;
		[SerializeField] private GameObject explosionPrefab;
		private float GetMaxHealth() => stats.GetMaxHealth();
		private float GetMaxShield() => stats.GetMaxShield();

		private float currentShield;


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

		public void TakeDamage(float amount, Vector3 hitPoint)
		{
			if (amount > 0) onTakeDamage?.Invoke();
			if (!stats.GetIsShieldUnlocked()) TakeHealthDamage(amount, hitPoint);
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
					TakeHealthDamage(amount, hitPoint);
				}
			}
		}

		private void TakeHealthDamage(float amount, Vector3 hitPoint)
		{
			currentHealth -= amount;
			if (currentHealth <= 0)
			{
				currentHealth = 0;
				Die();
			}

			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
			Explode(hitPoint);
		}

		public void Die()
		{
			if (isDead) return;
			isDead = true;
			Debug.Log(stats.characterName + " died");
			onDeath?.Invoke(this);
			GameManager.Instance.ChangeState(GameState.Dead);
		}

		private void Explode(Vector3 hitPoint) => Instantiate(explosionPrefab, hitPoint, Quaternion.identity);


		public void Heal(float amount)
		{
			currentHealth += amount;
			if (currentHealth > stats.GetMaxHealth()) currentHealth = stats.GetMaxHealth();
			onHealthChanged?.Invoke(currentHealth, GetMaxHealth());
		}

		public void HealShields(float amount)
		{
			currentShield += amount;
			if (currentShield > stats.GetMaxShield()) currentShield = stats.GetMaxShield();
			onShieldChanged?.Invoke(currentShield, GetMaxShield());
		}
	}
}