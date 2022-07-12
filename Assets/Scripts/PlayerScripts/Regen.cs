//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.Collections;
using UnityEngine;
using Upgrades;

namespace PlayerScripts
{
	/// <summary>
	///Regen functions for Health/Shields
	/// </summary>
	public class Regen : MonoBehaviour
	{
		private PlayerHealth player;
		[SerializeField] private Stat healthRegenAmount;
		[SerializeField] private Stat shieldRegenAmount;
		[SerializeField] private Stat healthRegenFreq;
		[SerializeField] private Stat shieldRegenFreq;
		private GameState state;


		private void Awake() => player = GetComponent<PlayerHealth>();

		private void Start()
		{
			StartCoroutine(RegenCor());
		}

		private IEnumerator RegenCor()
		{
			float timerH = 0;
			float timerS = 0;

			while (!player.GetIsDead())
			{
				var x = GameManager.GetCurrentState();
				if (GameManager.GetCurrentState() == GameState.InGame)
				{
					timerH += Time.deltaTime;
					timerS += Time.deltaTime;

					if (timerH >= healthRegenFreq.runTimeValue)
					{
						player.Heal(healthRegenAmount.runTimeValue);
						Debug.Log("Healing health at " + timerH + " @ " + Time.realtimeSinceStartup);
						timerH = 0;
					}

					if (timerS >= shieldRegenFreq.runTimeValue)
					{
						player.HealShields(shieldRegenAmount.runTimeValue);
						Debug.Log("Healing Shields at " + timerS + " @ " + Time.realtimeSinceStartup);
						timerS = 0;
					}

				}
				yield return null;

			}
		}
	}
}