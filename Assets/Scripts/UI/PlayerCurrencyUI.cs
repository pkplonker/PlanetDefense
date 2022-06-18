using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerCurrencyUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI tmp;
		private void Awake()=>tmp = GetComponentInChildren<TextMeshProUGUI>();
		private void OnEnable()=>CurrencyHandler.onCurrencyChanged += UpdateUI;
		private void OnDisable()=>CurrencyHandler.onCurrencyChanged -= UpdateUI;
		
		private void UpdateUI(ulong amount)
		{
			if (tmp == null) return;
			tmp.text = "$" + amount;
		}
	}
}