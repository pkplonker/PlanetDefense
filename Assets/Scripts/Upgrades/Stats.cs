using UnityEngine;

namespace Upgrades
{
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
}
