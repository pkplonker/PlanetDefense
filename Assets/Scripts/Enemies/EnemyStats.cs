using System;
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
		public string projectileDataPath;
		public string spritePath;
		private ProjectileData projectileData;
		private Sprite sprite;

		private void OnEnable()
		{
			projectileData = Resources.Load<ProjectileData>(projectileDataPath);
			sprite = Resources.Load<Sprite>(spritePath) as Sprite;
		}

		public ProjectileData GetProjectileData() => projectileData;
		public Sprite GetSprite() => sprite;
	}
}