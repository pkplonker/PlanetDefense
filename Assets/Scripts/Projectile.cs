using Interfaces;
using StuartHeathTools;
using UnityEngine;
using Upgrades;

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
	private IRegisterDestroy shooter;

	public void Init(IRegisterDestroy shooter, ProjectileData data, Stats.Team targetTeam, Transform target)
	{
		Invoke(nameof(DestroyEntity), data.lifeTime);
		this.target = target;
		this.targetTeam = targetTeam;
		cachedTargetPosition = target.position;
		init = true;
		this.data = data;
		audioSource = GetComponent<AudioSource>();
		this.shooter = shooter;
	}

	public void Init(IRegisterDestroy shooter, ProjectileData data, Stats.Team targetTeam, Vector3 direciton)
	{
		Invoke(nameof(DestroyEntity), data.lifeTime);
		this.targetTeam = targetTeam;
		init = true;
		this.data = data;
		audioSource = GetComponent<AudioSource>();
		targetDirection = direciton;
		this.shooter = shooter;
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
		other.GetComponent<IDamageable>().TakeDamage(data.GetDamage());
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
			Vector3.MoveTowards(transform.position, target.position, data.GetSpeed() * Time.deltaTime);
	}

	private void NonLockOnUpdate()
	{
		var position = transform.position;
		Vector3 targetPosition;
		if (target != null) targetPosition = target.position;
		else targetPosition = targetDirection * 20;

		position += (targetPosition - position).normalized * data.GetSpeed() * Time.deltaTime;
		transform.position = position;
		var angle = UtilityMath.GetAngleFromVector((position - targetPosition).normalized);
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
	}


	public void DestroyEntity()
	{
		if (gameObject == null) return;
		Destroy(gameObject);
		shooter.RegisterDestroy(this);
	}
}