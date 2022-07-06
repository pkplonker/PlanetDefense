using System;
using Enemies;
using StuartHeathTools;
using UnityEngine;

public class GameManager : GenericUnitySingleton<GameManager>
{
	private static GameState gameState = GameState.Menu;
	public static event Action<GameState> onStateChange;
	public static event Action<int> onWaveStart;
	[SerializeField] private WaveContainer waveContainer;
	[SerializeField] private GameState defaultState= GameState.Menu;
	static int currentWave = -1;
	[SerializeField] private int kills;


	private void OnEnable()
	{
		onStateChange += GameStateChange;
		EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}

	private void Start()
	{
		SetDefaultState();
		currentWave = 0;
		EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}

	private void OnDisable()
	{
		onStateChange -= GameStateChange;
		EnemySpawner.OnEnemyDeath += EnemyDeath;
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



	private void SetDefaultState() => ChangeState(defaultState);


	public void ChangeState(GameState state)
	{
		if (gameState == GameState.Shop && state == GameState.WaveOver) return;
		gameState = state;
		onStateChange?.Invoke(gameState);
		switch (state)
		{
			case GameState.NewGame:
				SetupNewGame();
				ChangeState(GameState.NewWave);
				break;
			case GameState.NewWave:
				ChangeState(GameState.InGame);
				break;
			case GameState.WaveOver:
				if (waveContainer.IsLastWave(currentWave+1)) ChangeState(GameState.Menu); //placeholder functionality
				else ChangeState(GameState.Shop);
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