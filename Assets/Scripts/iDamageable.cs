using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface iDamageable
{
	public void TakeDamage(float amount);
	public void Die();
}