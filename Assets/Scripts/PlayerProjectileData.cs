using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Projectile", menuName = "Player Projectile")]

public class PlayerProjectileData : ProjectileData
{
  public float attackCooldown = 2f;
  public float range = 5f;
}
