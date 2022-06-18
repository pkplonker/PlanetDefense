using System;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
	[SerializeField] private PlayerStats stats;
	public static CurrencyHandler instance;
	[SerializeField] private ulong startingCurrency = 0;
	public static event Action<ulong> onCurrencyChanged;

	private void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		stats.currency = startingCurrency;
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
		if (state != GameState.GameOver) return;
		stats.currency = startingCurrency;
		onCurrencyChanged?.Invoke(stats.currency);
	}

	public void AddMoney(ulong amount)
	{
		stats.currency += amount;
		onCurrencyChanged?.Invoke(stats.currency);
	}

	public bool RemoveMoney(ulong amount)
	{
		if (stats.currency < amount) return false;
		stats.currency -= amount;
		onCurrencyChanged?.Invoke(stats.currency);
		return true;
	}

	public bool CanAfford(ulong amount) => stats.currency >= amount;
	private void EnemyDeath(EnemyStats stats) => AddMoney(stats.currencyValue);
}