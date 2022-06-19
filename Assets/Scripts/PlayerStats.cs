using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/Player Stats")]

public class PlayerStats : Stats
{
	public ulong currency;
	public Stat health;
	public float GetMaxHealth() => health.GetCurrentValue();
	
}

