using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameState GameState;
}

public enum GameState
{
	Menu,
	InGame,
	Dead,
	Paused,
	GameOver
}