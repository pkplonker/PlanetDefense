using System;
using Interfaces;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "New Ability",menuName = "Abilities/Small Cannon")]
    public class SmallCannon : Ability
    {
        [SerializeField] ProjectileData projectileData;
        [SerializeField] Projectile projectilePrefab;



    }
}
