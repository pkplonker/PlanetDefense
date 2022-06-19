using UnityEngine;

namespace PlayerScripts
{
	[CreateAssetMenu(fileName = "New Manual Data", menuName = "Data/Manual Weapon Data")]

	public class ManualWeaponData : ScriptableObject
	{
		public float damage;
		public float range;
		public float fireRate;
	}
}
