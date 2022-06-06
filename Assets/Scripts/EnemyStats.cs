using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/Enemy Stats")]

public class EnemyStats : Stats
{
	public float speed = 2f;
	public float attackRange = 3f;
	public float damage = 3f;
	public uint value = 3;
	public ProjectileData projectileData;
	public float attackSpeed;
}
