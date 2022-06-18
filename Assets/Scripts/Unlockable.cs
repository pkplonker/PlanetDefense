using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable", menuName = "Unlockable/New unlockable")]
public class Unlockable : ScriptableObject, IBuyable
{
	public bool isUnlocked;
	public ulong price;
	public void Buy() => isUnlocked = true;
	private void ResetData() => isUnlocked = false;

	private void OnEnable()
	{
		ResetData();
		GameManager.onStateChange += OnStateChange;
	}

	private void OnStateChange(GameState state)
	{
		if (state == GameState.NewGame) ResetData();
	}
}