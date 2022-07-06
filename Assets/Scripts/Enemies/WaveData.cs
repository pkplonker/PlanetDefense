using System;
using System.Collections;
using System.Collections.Generic;
using Enemies;
using UnityEngine;

[CreateAssetMenu(fileName = "New Wave Data", menuName = "Waves/Wave Data")]
public class WaveData : ScriptableObject
{
	public List<Spawn> spawns;
	public int levelIndex;
	public int GetSpawnLength() => spawns.Count;
	public Spawn GetSpawnByIndex(int index) => index >= spawns.Count ? null : spawns[index];

	private void OnEnable()
	{
		foreach (var spawn in spawns)
		{
			spawn.enemyStats = Resources.Load<EnemyStats>(spawn.enemyStatsPath);
		}
	}
}

[Serializable]
public class Spawn
{
	public string enemyStatsPath;
	public float nextMobDelay;
	public EnemyStats enemyStats;

}