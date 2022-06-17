using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat", menuName = "Stats/New Stat")]
public class Stat : ScriptableObject
{
	public string statName;

	public float value;
	[Range(0, 5)] private float valueModifier = 1.1f;
	public uint currentCost;
	[Range(1, 5)] public uint costMultiplier;
	public int level;

	public virtual void Upgrade()
	{
		value *= valueModifier;
		currentCost *= costMultiplier;
		level++;
	}
}