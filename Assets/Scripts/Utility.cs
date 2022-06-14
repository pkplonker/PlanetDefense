using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static int RandomSign() => Random.value < 0.5f ? 1 : -1;

	public static bool RandomBool() => Random.value < 0.5f;
}