using System;
using UnityEngine;

namespace PlayerScripts
{
	public class ShieldSpriteController : MonoBehaviour
	{
		private SpriteRenderer spriteRenderer;
		[SerializeField] private PlayerHealth player;
		private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
		private void OnEnable() => player.onShieldChanged += OnShieldChanged;
		private void OnDisable() => player.onShieldChanged -= OnShieldChanged;

		private void Start()=>OnShieldChanged(0, 0);
		

		private void OnShieldChanged(float current, float max) =>
			spriteRenderer.enabled =
				player.IsShieldUnlocked() && current > 0 && GameManager.GetCurrentState() == GameState.InGame;
	}
}