using System;
using System.Collections;
using System.Collections.Generic;
using Interfaces;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour, IDestroyable
{
	private Stats.Team targetTeam;
	private ProjectileData data;
	private Transform target;
	private Vector3 direction;
	private bool init;
	private AudioSource audioSource;
	[SerializeField] private AudioClip impactSound;

	public void Init(ProjectileData data, Stats.Team targetTeam, Transform target)
	{
		Invoke(nameof(DestroyEntity), data.lifeTime);
		this.target = target;
		direction = (target.position - transform.position).normalized;
		transform.LookAt(target);
		this.targetTeam = targetTeam;
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
		IGetStats stats = other.GetComponent<IGetStats>();
		if (stats == null) return;
		if (stats.GetStats().team != targetTeam) return;
		other.GetComponent<IDamageable>().TakeDamage(data.damage);
		PlayImpactSound();
		DestroyEntity();
	}

	private void PlayImpactSound()
	{
		if (audioSource == null || impactSound == null) return;
		audioSource.PlayOneShot(impactSound);
	}

	private void Update()
	{
		if (GameManager.GetCurrentState() != GameState.InGame) return;

		if (!init) return;
		if (transform == null) transform.Translate(direction * data.speed * Time.deltaTime);
		else
		{
			switch (data.weaponType)
			{
				case WeaponType.LockOn:
					transform.position =
						Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
					break;
				case WeaponType.NonLockOn:
					transform.Translate(direction * data.speed * Time.deltaTime);
					break;
			}
		}
	}


	public void DestroyEntity()
	{
		if (gameObject == null) return;
		Destroy(gameObject);
	}
}