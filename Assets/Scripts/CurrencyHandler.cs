using System;
using Enemies;
using PlayerScripts;
using UnityEngine;
using Upgrades;

public class CurrencyHandler : MonoBehaviour
{
	[SerializeField] private PlayerStats stats;
	public static CurrencyHandler instance;
	[SerializeField] private long startingCurrency = 0;

	[SerializeField] private Stat moneyMultiplier;
	public static event Action<long> onCurrencyChanged;

	private void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
#if UNITY_EDITOR
		stats.currency = startingCurrency;
		//Logger.Log("Setting currency to starting currency");

#else
		stats.currency = 0;
			//			Logger.Log("Setting currency to 0");

#endif
		onCurrencyChanged?.Invoke(stats.currency);
	}

	private void Start() => onCurrencyChanged?.Invoke(stats.currency);

	private void OnEnable()
	{
		GameManager.onStateChange += HandleGameStateChange;
		EnemySpawner.OnEnemyDeath += EnemyDeath;
	}

	private void OnDisable()
	{
		GameManager.onStateChange -= HandleGameStateChange;
		EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}

	private void HandleGameStateChange(GameState state)
	{
		if (state != GameState.GameOver && state != GameState.Dead && state != GameState.Complete) return;
		stats.currency = startingCurrency;
		onCurrencyChanged?.Invoke(stats.currency);

	}

	public void AddMoney(long amount)
	{
		if (amount == long.MaxValue) stats.currency = amount;
		else stats.currency += amount * (long) moneyMultiplier.GetCurrentValue();
		onCurrencyChanged?.Invoke(stats.currency);
	}

	public bool RemoveMoney(long amount)
	{
		if (stats.currency < amount) return false;
		stats.currency -= amount;
		onCurrencyChanged?.Invoke(stats.currency);
		return true;
	}

	public bool CanAfford(long amount) => stats.currency >= amount;
	private void EnemyDeath(EnemyStats stats) => AddMoney(stats.currencyValue);
}