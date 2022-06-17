using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Manual Data", menuName = "Data/Manual Weapon Data")]

public class ManualWeaponData : ScriptableObject
{
	public float damage;
	public float range;
	public float fireRate;
}
