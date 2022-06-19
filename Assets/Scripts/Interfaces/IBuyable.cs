using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuyable
{
	public string GetStatName();
	public void Buy();
	public ulong GetCurrentCost();
	public string GetLevel();
}