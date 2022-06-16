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
		if (transform == null) transform.Translate(target.position - transform.position * data.speed * Time.deltaTime);
		else
		{
			switch (data.weaponType)
			{
				case WeaponType.LockOn:
					transform.position =
						Vector3.MoveTowards(transform.position, target.position, data.speed * Time.deltaTime);
					break;
				case WeaponType.NonLockOn:
					Debug.DrawRay(transform.position, (target.position - transform.position).normalized , Color.magenta);
					transform.position+= (target.position - transform.position).normalized * data.speed * Time.deltaTime;
					
					float angle = Utility.GetAngleFromVector((transform.position-target.position).normalized);
					transform.rotation =  Quaternion.Euler(new Vector3(0, 0, angle));
					
					//transform.eulerAngles = new Vector3(0,0,angle);
					break;
			}
		}
	}

	private void OnDrawGizmos()
	{
		if (target == null) return;
		Gizmos.color = Color.magenta;
		Gizmos.DrawIcon(target.transform.position, "target", true, Color.magenta);
		
	}

	public void DestroyEntity()
	{
		if (gameObject == null) return;
		Destroy(gameObject);
	}
}