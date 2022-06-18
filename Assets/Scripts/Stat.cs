using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Unlockable/New Stat")]
public class Stat : ScriptableObject, IBuyable
{
	[SerializeField] private string statName;

	[SerializeField] private float value;
	[Range(0, 5)] [SerializeField] private float valueModifier = 1.1f;
	[SerializeField] private ulong currentCost;
	[Range(1, 5)] [SerializeField] private uint costMultiplier;
	[SerializeField] private int level;


	private float runTimeValue;
	private ulong runTimeCurrentCost;
	private int runTimeLevel;

	private void OnEnable()
	{
		ResetData();
		GameManager.onStateChange += OnStateChange;
	}

	private void OnStateChange(GameState state)
	{
		if (state == GameState.NewGame) ResetData();
	}

	private void ResetData()
	{
		runTimeValue = value;
		runTimeLevel = level;
		runTimeCurrentCost = currentCost;
	}



	public ulong GetCurrentCost() => runTimeCurrentCost;

	public string GetStatName() => statName;

	public object GetLevel() => runTimeLevel;

	public float GetCurrentValue() => runTimeValue;
	public void Buy()
	{
		runTimeValue *= valueModifier;
		runTimeCurrentCost *= costMultiplier;
		runTimeLevel++;
	}
}