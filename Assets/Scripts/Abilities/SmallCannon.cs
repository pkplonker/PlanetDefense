using System;
using Interfaces;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "New Ability",menuName = "Abilities/Small Cannon")]
    public class SmallCannon : Ability,IShootable
    {
        [SerializeField] ProjectileData projectileData;
        [SerializeField] Projectile projectilePrefab;
        private float lastShotTime = 0;
        private void OnEnable()
        {
	        lastShotTime = 0;
        }

        public Projectile Shoot(Transform shooter, Stats.Team targetTeam, Transform targetTransform)
        {
	        if (lastShotTime + cooldown > Time.deltaTime) return null;
	         if (targetTransform.GetComponent<ICheckAlive>().GetIsDead()) return null;
	         Projectile projectile = Instantiate(projectilePrefab, shooter).GetComponent<Projectile>();
	         projectile.Init(projectileData, Stats.Team.Player, targetTransform);
	         Debug.Log("Spawned projectile");
	         lastShotTime = Time.time;
	         return projectile;
         }

        
    }
}
