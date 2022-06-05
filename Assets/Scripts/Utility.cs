using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utility
{
	public static int RandomSign()
	{
		return Random.value < 0.5f ? 1 : -1;
	}
	public static bool RandomBool()
	{
		return Random.value < 0.5f ? true : false;
	}
}