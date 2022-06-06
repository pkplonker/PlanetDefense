using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR;

public class GameManager : MonoBehaviour
{
	private static GameState gameState = GameState.InGame;
	public static GameManager instance;
	public static event Action<GameState> onStateChange;
	[SerializeField] private GameState defaultState;
	private void Awake()
	{
		if (instance == null)
		{
			instance = this;
		}
		else if (instance != this)
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		SetDefaultState();
	}

	private void SetDefaultState()
	{
		ChangeState(defaultState);
	}

	public static void ChangeState(GameState state)
	{
		gameState = state;
		onStateChange?.Invoke(gameState);
		if (state == GameState.NewGame)
		{
			ChangeState(GameState.InGame);
		}
		if(state== GameState.WaveOver)
		{
			ChangeState(GameState.Shop);
		}

		if (state == GameState.NewWave)
		{
			ChangeState(GameState.InGame);
		}
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
	NewWave
}