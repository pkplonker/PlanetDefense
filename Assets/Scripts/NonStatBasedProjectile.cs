using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New non-stat Projectile", menuName = "Projectiles/Non-Stat Projectile")]
public class NonStatBasedProjectile : ProjectileData
{
	[SerializeField] private float damage;
	[SerializeField] private float speed;
	[SerializeField] private float cooldown;
	[SerializeField] private float range;

	public override float GetDamage() => damage;
	public override float GetSpeed() => speed;
	public override float GetCooldown() => cooldown;
	public override float GetRange()=> range;
}