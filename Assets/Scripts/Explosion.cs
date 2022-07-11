using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{

	//animation event
	public void Finished()=>Destroy(gameObject);
	
}