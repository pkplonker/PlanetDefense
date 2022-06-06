using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{

	private void OnEnable()
	{
		GameManager.onStateChange += GameManagerOnonStateChange;
	}


	private void OnDisable()
	{
		GameManager.onStateChange -= GameManagerOnonStateChange;
	}

	private void GameManagerOnonStateChange(GameState state)
	{
		if (state == GameState.Paused)
		{
			PauseTime();
		}
		else
		{
			ResumeTime();
		}
	}

	private void PauseTime()
	{
	
	}

	private void ResumeTime()
	{
		
	}
}