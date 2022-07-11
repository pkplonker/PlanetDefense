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
		private Coroutine cor;
		[SerializeField] private Stat healthRegenAmount;
		[SerializeField] private Stat shieldRegenAmount;
		[SerializeField] private Stat healthRegenFreq;
		[SerializeField] private Stat shieldRegenFreq;
		private GameState state;
		
		
		private void Awake() => player = GetComponent<PlayerHealth>();
		
		private void Update()
		{
			if (GameManager.GetCurrentState() != GameState.InGame) return;
		}

		private IEnumerator RegenCor()
		{
			float timerH = 0;
			float timerS = 0;

			while (!player.GetIsDead())
			{
				if (GameManager.GetCurrentState() != GameState.InGame) yield return null;
				timerH += healthRegenFreq.runTimeValue;
				timerS += healthRegenFreq.runTimeValue;

				if (timerH >= healthRegenFreq.runTimeValue)
				{
					player.Heal(healthRegenAmount.runTimeValue);
				}
				if (timerS >= shieldRegenFreq.runTimeValue)
				{
					player.HealShields(shieldRegenAmount.runTimeValue);
				}

				yield return null;
			}
		}
	}
}