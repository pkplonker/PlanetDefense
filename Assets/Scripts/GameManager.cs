using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	private static GameState gameState = GameState.InGame;
	public static GameManager instance;
	public static event Action<GameState> onStateChange;
	public static event Action<int> onWaveStart;
	[SerializeField] private WaveContainer waveContainer;
	[SerializeField] private GameState defaultState;
	static int currentWave = -1;
	[SerializeField] private int kills;


	private void OnEnable()
	{
		onStateChange += GameStateChange;
		Enemies.EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}

	private void Start()
	{
		currentWave = 0;
		Enemies.EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}

	private void OnDisable()
	{
		onStateChange -= GameStateChange;
		Enemies.EnemySpawner.OnEnemyDeath += EnemyDeath;
	}

	public void EnemyDeath(EnemyStats stats)
	{
		kills++;
		if (kills >= waveContainer.GetWaveByIndex(currentWave).GetSpawnLength()) ChangeState(GameState.WaveOver);
	}


	private void GameStateChange(GameState state)
	{
		if (state != GameState.NewWave) return;
		kills = 0;
		IncrementWave();
	}

	private void Awake()
	{
		if (instance == null) instance = this;
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}
		DontDestroyOnLoad(gameObject);
		SetDefaultState();
	}

	private void SetDefaultState() => ChangeState(defaultState);


	public static void ChangeState(GameState state)
	{
		gameState = state;
		onStateChange?.Invoke(gameState);
		switch (state)
		{
			case GameState.NewGame:
				SetupNewGame();
				ChangeState(GameState.NewWave);
				break;
			case GameState.WaveOver:
				ChangeState(GameState.Shop);
				break;
			case GameState.NewWave:
				ChangeState(GameState.InGame);
				break;
		}
	}

	private static void SetupNewGame() => currentWave = -1;

	private void IncrementWave()
	{
		currentWave++;
		if (waveContainer.IsLastWave(currentWave))
		{
			ChangeState(GameState.Complete);
			return;
		}

		onWaveStart?.Invoke(currentWave);
	}

	public static GameState GetCurrentState() => gameState;
}


public enum GameState
{
	Menu,
	InGame,
	Dead,
	Paused,
	GameOver,
	NewGame,
	WaveOver,
	Shop,
	NewWave,
	Complete
}