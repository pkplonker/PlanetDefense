using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Projectile : MonoBehaviour, IDestroyable
{
	private Stats.Team targetTeam;
	private ProjectileData data;
	private Transform target;
	private bool init;
	private AudioSource audioSource;
	[SerializeField] private AudioClip impactSound;
	private Vector3 cachedTargetPosition;

	public void Init(ProjectileData data, Stats.Team targetTeam, Transform target)
	{
		Invoke(nameof(DestroyEntity), data.lifeTime);
		this.target = target;
		this.targetTeam = targetTeam;
		cachedTargetPosition = target.position;
		init = true;
		this.data = data;
		audioSource = GetComponent<AudioSource>();
	}

	private void OnEnable() => GameManager.onStateChange += StateChange;
	private void OnDisable() => GameManager.onStateChange -= StateChange;

	private void StateChange(GameState state)
	{
		if (state == GameState.Paused) DestroyEntity();
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		var stats = other.GetComponent<IGetStats>();
		if (stats == null) return;
		if (stats.GetStats().team != targetTeam) return;
		other.GetComponent<IDamageable>().TakeDamage(data.damage);
		SFXController.instance.Playclip(impactSound);
		DestroyEntity();
	}


	private void Update()
	{
		if (GameManager.GetCurrentState() != GameState.InGame) return;
		if (!init) return;
		if (transform == null) transform.Translate(target.position - transform.position  * data.speed * Time.deltaTime);
		else
		{
			switch (data.weaponType)
			{
				case WeaponType.LockOn:
					transform.position =
						Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
					break;
				case WeaponType.NonLockOn:
					Debug.DrawRay(transform.position, target.position - transform.position * 4, Color.magenta);
					//Debug.DrawRay(transform.position, direction * 4, Color.red);

					transform.localEulerAngles = new Vector3(0, 0, 0); //works for enemy
					//	transform.eulerAngles = new Vector3(0, 0, Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg);
					//transform.eulerAngles = Vector3.RotateTowards(transform.position, target.position, 7, 0);
					Quaternion rotation = Quaternion.LookRotation(target.position - transform.position, Vector3.up);
					transform.rotation = rotation;
					transform.Translate(target.position - transform.position  * data.speed * Time.deltaTime);
					break;
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (target != null)
		{
			Gizmos.color = Color.magenta;
			Gizmos.DrawIcon(target.transform.position, "target", true,Color.magenta);
		}
	}

	public void DestroyEntity()
	{
		if (gameObject == null) return;
		Destroy(gameObject);
	}
}