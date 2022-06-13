using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Waves/Wave Data")]
public class WaveData : ScriptableObject
{
	public List<Spawn> spawns;
	public int GetSpawnLength() => spawns.Count;
	
	public Spawn GetSpawnByIndex(int index)
	{
		return index >= spawns.Count ? null : spawns[index];
	}
}
[Serializable]
public class Spawn
{
	public EnemyStats enemy;
	public float nextMobDelay;
}