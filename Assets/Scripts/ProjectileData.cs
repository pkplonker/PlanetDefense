using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ProjectileData : ScriptableObject
{
	public float lifeTime;
	public WeaponType weaponType = WeaponType.NonLockOn;

	public abstract float GetDamage();
	public abstract float GetSpeed();
	public abstract float GetCooldown();

	public abstract float GetRange();
}

public enum WeaponType
{
	LockOn,
	NonLockOn
}