using UnityEngine;

namespace Interfaces
{
	public interface IDamageable
	{
		public void TakeDamage(float amount, Vector3 hitPoint);
		public void Die();
	}
}