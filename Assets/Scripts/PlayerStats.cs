using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/Player Stats")]
public class PlayerStats : Stats
{
	public ulong currency;
	public Stat health;
	public Stat shield;
	public Unlockable shieldUnlock;

	public float GetMaxHealth() => health.GetCurrentValue();
	public float GetMaxShield() => shield.GetCurrentValue();
	public bool GetIsShieldUnlocked() => shieldUnlock.GetIsUnlocked();
}