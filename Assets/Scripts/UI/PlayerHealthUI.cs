using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerHealthUI : MonoBehaviour
	{
		[Range(0, 1)] [SerializeField] private float lowHealthThreshold = 0.2f;
		[SerializeField] private Player player;
		[SerializeField] private Color lowHealthColor;
		private Color defaultColor;
		private TextMeshProUGUI tmp;
		
		private void Awake()
		{
			tmp = GetComponent<TextMeshProUGUI>();
			defaultColor = tmp.color;
		}
		private void OnEnable()=>player.onHealthChanged += UpdateUI;
		private void OnDisable()=>player.onHealthChanged -= UpdateUI;
		private void UpdateUI(float currentHealth, float maxHealth)
		{
			tmp.color = currentHealth / maxHealth < lowHealthThreshold ? lowHealthColor : defaultColor;
			tmp.text = (ulong)currentHealth + "/" + (ulong)player.GetMaxHealth();
		}
	}
}