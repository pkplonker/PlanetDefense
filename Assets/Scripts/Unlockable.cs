using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unlockable", menuName = "Unlockable/New unlockable")]
public class Unlockable : Purchaseable
{
	[SerializeField] private string statName;
	[SerializeField] private bool isOneTimePurchase = false;

	[SerializeField] private bool isUnlocked;
	public ulong price;

	public override string GetStatName() => statName;
	public override void Buy() => isUnlocked = true;
	public override ulong GetCurrentCost() => price;
	public override bool GetIsOneTimePurchase() => isOneTimePurchase;
	public bool GetIsUnlocked() => isUnlocked;
	public override string GetLevel() => "";

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