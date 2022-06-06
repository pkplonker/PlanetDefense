using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
	private PlayerStats stats;
	[SerializeField] private Projectile projectilePrefab;
	[SerializeField] private PlayerProjectileData attack;
	[SerializeField] private List<Ability> abilities;
	private List<Ability> useableAbilities;
	private List<Projectile> projectiles = new List<Projectile>();
	private float lastShotTime;

	private void Awake()
	{
		stats = (PlayerStats) GetComponent<Player>().GetStats();
	}

	private void Start()
	{
		foreach (var a in abilities)
		{
			useableAbilities.Add(Instantiate(a));
		}
	}

	private void Update()
	{
		if (GameManager.GetCurrentState() != GameState.InGame) return;
		Transform target = AcquireTarget(attack);
		if (lastShotTime + attack.attackCooldown < Time.time && target!=null)
		{
			projectiles.Add(Shoot(transform, Stats.Team.Enemy,  target));
			
		}
		
	}

	private Projectile Shoot(Transform shooter, Stats.Team targetTeam, Transform targetTransform)
	{
		if (shooter == null || targetTransform == null)
		{
			Debug.LogWarning("Missing info");
			return null;
		}
		if (targetTransform.GetComponent<ICheckAlive>().GetIsDead()) return null;
		Projectile projectile = Instantiate(projectilePrefab, shooter).GetComponent<Projectile>();
		projectile.Init(attack, Stats.Team.Enemy, targetTransform);
		Debug.Log("Spawned projectile");
		lastShotTime = Time.time;
		return projectile;
	}
	private Transform AcquireTarget(PlayerProjectileData playerProjectileData)
	{
		Debug.Log("Acquiring target");

		RaycastHit2D hit = Physics2D.CircleCast(transform.position, playerProjectileData.range, Vector2.up, 0.1f);
		if (hit.collider == null) return null;
		if (!hit.collider.GetComponent<Enemy>()) return null;
		Debug.Log("Target found" +hit.transform.name);
		return hit.collider.transform;
	}
}