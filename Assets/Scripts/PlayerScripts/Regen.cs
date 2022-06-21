//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.Collections;
using UnityEngine;

namespace PlayerScripts
{
	/// <summary>
	///Regen functions for Health/Shields
	/// </summary>
	public class Regen : MonoBehaviour
	{
		private PlayerController player;
		private Coroutine cor;
		private void Awake() => player = GetComponent<PlayerController>();

		private void OnEnable() => GameManager.onStateChange += StateChange;
		private void OnDisable() => GameManager.onStateChange += StateChange;

		private void StateChange(GameState obj)
		{
		}

		private void Start()
		{
		}

		private void Update()
		{
			if (GameManager.GetCurrentState() != GameState.InGame) return;
		}
/*
		private IEnumerator RegenCor()
		{
			while (player.GetIsDead())
			{
			}
		}*/
	}
}