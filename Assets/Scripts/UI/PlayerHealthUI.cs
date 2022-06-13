using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerHealthUI : MonoBehaviour
	{
		private TextMeshProUGUI tmp;
		[SerializeField] private Player player;
		[SerializeField] private Color lowHealthColor;
		private Color defaultColor;
		[Range(0, 1)] [SerializeField] private float lowHealthThreshold = 0.2f;

		private void Awake()
		{
			tmp = GetComponent<TextMeshProUGUI>();
			defaultColor = tmp.color;
		}

		private void OnEnable()
		{
			player.onHealthChanged += UpdateUI;
		}

		private void OnDisable()
		{
			player.onHealthChanged -= UpdateUI;
		}

		private void UpdateUI(float currentHealth, float maxHealth)
		{
			tmp.color = currentHealth / maxHealth < lowHealthThreshold ? lowHealthColor : defaultColor;
			tmp.text = currentHealth + "/" + player.GetMaxHealth();
		}
	}
}