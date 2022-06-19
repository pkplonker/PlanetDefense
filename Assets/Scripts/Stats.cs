using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Stats : ScriptableObject
{
	public string characterName = "New character";
	public Team team;
	
	public enum Team
	{
		Player,
		Enemy
	}
}
