using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Abilities;
using Interfaces;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
	private PlayerStats stats;
	[SerializeField] private Ability attack;
	[SerializeField] private List<Ability> abilities;
	private List<Ability> useableAbilities;
	private List<Projectile> projectiles = new List<Projectile>();

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
		if (GameManager.gameState != GameState.InGame) return;
		Transform target = AcquireTarget();
		if (target == null) return;
		foreach (var ability in useableAbilities.Where(ability => ability != null))
		{
			if (ability is IShootable iS)
			{
				Projectile p = iS.Shoot(transform, Stats.Team.Enemy, target);
				if (p == null)
				{
					continue;
				}
				projectiles.Add(p);
				break;
			}
			var iU = ability as IUseable;
			if (iU == null) continue;
			if (iU.Use()) break;
			
		}
	}

	private Transform AcquireTarget()
	{
		Debug.Log("Acquiring target");
		return null;
	}
}