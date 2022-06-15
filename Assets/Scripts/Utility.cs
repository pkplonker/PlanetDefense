using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static int RandomSign() => Random.value < 0.5f ? 1 : -1;

	public static bool RandomBool() => Random.value < 0.5f;

	public static float GetAngleFromVector(Vector3 direction)
	{
		direction = direction.normalized;
		var angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
		if (angle < 0) angle += 360;
		return angle + 90;
	}
}