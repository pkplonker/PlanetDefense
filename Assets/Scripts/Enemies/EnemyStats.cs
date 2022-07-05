using UnityEngine;
using Upgrades;

namespace Enemies
{
	[CreateAssetMenu(fileName = "New Stats", menuName = "Stats/Enemy Stats")]

	public class EnemyStats : Stats
	{
		public float maxHealth;
		public float movementSpeed = 2f;
		public float impactDamage = 3f;
		public uint currencyValue = 3;
		public ProjectileData projectileData;
		public Sprite sprite;
	}
}
