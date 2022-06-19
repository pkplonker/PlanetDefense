using UnityEngine;

public class ShieldSpriteController : MonoBehaviour
{
	private SpriteRenderer spriteRenderer;
	[SerializeField] private Player player;
	private void Awake() => spriteRenderer = GetComponent<SpriteRenderer>();
	private void OnEnable() => player.onShieldChanged += OnShieldChanged;
	private void OnShieldChanged(float current, float max) =>
		spriteRenderer.enabled = player.IsShieldUnlocked() && current > 0;
	private void OnDisable() => player.onShieldChanged += OnShieldChanged;
}