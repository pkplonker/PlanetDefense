//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using UnityEngine;

namespace PlayerScripts
{
	/// <summary>
	///PlayerSprite full description
	/// </summary>
	public class PlayerSprite : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		private void OnEnable() => GameManager.onStateChange += HandleGameStateChange;
		private void OnDisable() => GameManager.onStateChange -= HandleGameStateChange;

		private void Awake()
		{
			spriteRenderer = GetComponentInChildren<SpriteRenderer>();
			spriteRenderer.enabled = false;
		}


		private void HandleGameStateChange(GameState state)
		{
			if (state == GameState.NewGame) spriteRenderer.enabled = true;
		}
	}
}