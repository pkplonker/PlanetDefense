using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Projectile", menuName = "Projectile")]
public class ProjectileData : ScriptableObject
{
   
   public float damage;
   public float speed;
   public float lifeTime;
   public WeaponType weaponType = WeaponType.NonLockOn;
   public float rotationSpeed=360f;
}

public enum WeaponType
{
   LockOn,
   NonLockOn
}
