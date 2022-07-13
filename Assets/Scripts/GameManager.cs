using System;
using Enemies;
using StuartHeathTools;
using UI;
using UnityEngine;

public class GameManager : GenericUnitySingleton<GameManager>
{
	private static GameState gameState = GameState.Menu;
	public static event Action<GameState> onStateChange;
	public static event Action<int> onWaveStart;
	[SerializeField] private WaveContainer waveContainer;
	[SerializeField] private GameState defaultState = GameState.Menu;
	private static int currentWave = -1;
	[SerializeField] private int kills;
	[SerializeField] private StoryUI story;
	private static bool newGame;
	private void OnEnable() => EnemySpawner.OnEnemyDeath -= EnemyDeath;
	private void OnDisable() => EnemySpawner.OnEnemyDeath += EnemyDeath;
	private void SetDefaultState() => ChangeState(defaultState);


	public static GameState GetCurrentState() => gameState;

	private void Start()
	{
		SetDefaultState();
		currentWave = 0;
		EnemySpawner.OnEnemyDeath -= EnemyDeath;
	}
	private static void SetupNewGame()
	{
		newGame = true;
		currentWave = -1;
	} 
	public void EnemyDeath(EnemyStats stats)
	{
		kills++;
		if (kills >= waveContainer.GetWaveByIndex(currentWave).GetSpawnLength()) ChangeState(GameState.WaveOver);
	}


	public void ChangeState(GameState state)
	{

		Logger.Log("GM state = " + state);
		if (gameState == GameState.Shop && state == GameState.NewWave)
		{
			kills = 0;
			IncrementWave();
			ChangeState(GameState.InGame);
			

			return;
		}
		gameState = state;

		onStateChange?.Invoke(gameState);
		switch (state)
		{
			case GameState.InGame:
				newGame = false;
				break;
			case GameState.NewGame:
				SetupNewGame();
				if (PlayerPrefs.GetInt("Story") == 1) story.PlayInitialStory();
				else ChangeState(GameState.NewWave);
				break;
			case GameState.NewWave:
				if (PlayerPrefs.GetInt("Story") == 1 && !newGame)
				{
					ChangeState(GameState.Story);
					story.PlayLevelStory(currentWave);
				}
				else
				{
					kills = 0;
					IncrementWave();
					ChangeState(GameState.InGame);
				}

				break;
			case GameState.WaveOver:
				if (waveContainer.IsLastWave(currentWave + 1)) ChangeState(GameState.Complete);
				else if (PlayerPrefs.GetInt("Story") == 1)
				{
					ChangeState(GameState.Story);
					story.PlayLevelStory(currentWave);
				}
				else ChangeState(GameState.Shop);
				break;
		}
	}


	private void IncrementWave()
	{
		currentWave++;
		if (waveContainer.IsLastWave(currentWave)) ChangeState(GameState.Complete);
		onWaveStart?.Invoke(currentWave);
	}
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
	Complete,
	Story
}