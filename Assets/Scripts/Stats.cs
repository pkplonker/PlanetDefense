using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : ScriptableObject
{
	public float maxHealth;
	public string characterName = "New character";
	public Team team;
	
	public enum Team
	{
		Player,
		Enemy
	}
}
