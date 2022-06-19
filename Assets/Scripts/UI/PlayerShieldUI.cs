using PlayerScripts;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerShieldUI : MonoBehaviour
	{
		[Range(0, 1)] [SerializeField] private float lowHealthThreshold = 0.2f;
		[SerializeField] private PlayerController player;
		[SerializeField] private Color lowHealthColor;
		private Color defaultColor;
		private TextMeshProUGUI tmp;

		private void Awake()
		{
			tmp = GetComponent<TextMeshProUGUI>();
			defaultColor = tmp.color;
		}

		private void OnEnable() => player.onShieldChanged += UpdateUI;
		private void OnDisable() => player.onShieldChanged -= UpdateUI;

		private void UpdateUI(float currentHealth, float maxHealth)
		{
			tmp.color = currentHealth / maxHealth < lowHealthThreshold ? lowHealthColor : defaultColor;
			if (player.IsShieldUnlocked())
			{
				tmp.text = (ulong) currentHealth + "/" + (ulong) player.GetMaxShield();
			}
			else
			{
				tmp.text = "";
			}
		}
	}
}