using Interfaces;
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
	private Vector3 targetDirection;

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

	public void Init(ProjectileData data, Stats.Team targetTeam, Vector3 direciton)
	{
		Invoke(nameof(DestroyEntity), data.lifeTime);
		this.targetTeam = targetTeam;
		init = true;
		this.data = data;
		audioSource = GetComponent<AudioSource>();
		targetDirection = direciton;
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

		switch (data.weaponType)
		{
			case WeaponType.LockOn:
				LockOnUpdate();
				break;
			case WeaponType.NonLockOn:
				NonLockOnUpdate();
				break;
		}
	}

	private void LockOnUpdate()
	{
		transform.position =
			Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
	}

	private void NonLockOnUpdate()
	{
		var position = transform.position;
		Vector3 targetPosition;
		if (target != null) targetPosition = target.position;
		else targetPosition = targetDirection*10;
		
		position += (targetPosition - position).normalized * data.speed * Time.deltaTime;
		transform.position = position;
		var angle = Utility.GetAngleFromVector((position - targetPosition).normalized);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}


	public void DestroyEntity()
	{
		if (gameObject == null) return;
		Destroy(gameObject);
	}
}