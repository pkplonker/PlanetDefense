using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stat Projectile", menuName = "Projectiles/Stat Projectile")]
public class StatBasedProjectileData : ProjectileData
{
	[SerializeField] private Stat damageStat;
	[SerializeField] private Stat speedStat;
	[SerializeField] private Stat cooldownStat;
	[SerializeField] private Stat rangeStat;

	public override float GetDamage() => damageStat.value;
	public override float GetSpeed() => speedStat.value;
	public override float GetCooldown() => cooldownStat.value;
	public override float GetRange() => rangeStat.value;
}