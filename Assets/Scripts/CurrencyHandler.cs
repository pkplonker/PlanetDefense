using System;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
	[SerializeField] private PlayerStats stats;
	public static CurrencyHandler instance;
	[SerializeField] private uint startingCurrency = 0;
	[field: SerializeField] private uint DEFAULT_CURRENCY;
	public static event Action<uint> onCurrencyChanged;

	private void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		stats.currency = DEFAULT_CURRENCY;
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

	public void AddMoney(uint amount)
	{
		stats.currency += amount;
		onCurrencyChanged?.Invoke(stats.currency);
	}

	public bool RemoveMoney(uint amount)
	{
		if (stats.currency < amount) return false;
		stats.currency -= amount;
		onCurrencyChanged?.Invoke(stats.currency);
		return true;
	}

	public bool CanAfford(uint amount) => stats.currency >= amount;
	private void EnemyDeath(EnemyStats stats) => AddMoney(stats.currencyValue);
}