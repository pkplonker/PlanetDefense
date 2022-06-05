using UnityEngine;

namespace Interfaces
{
	public interface IShootable
	{
		public Projectile Shoot(Transform shooter, Stats.Team targetTeam, Transform targetTransform);
	}
}
