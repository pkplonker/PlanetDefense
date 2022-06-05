using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerCurrencyUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI tmp;
		[SerializeField] private CurrencyHandler currencyHandler;

		private void OnEnable()
		{
			currencyHandler.onCurrencyChanged += UpdateUI;
		}

		private void OnDisable()
		{
			currencyHandler.onCurrencyChanged -= UpdateUI;
		}
		

		private void UpdateUI(uint amount)
		{
			tmp.text = "$" + amount;
		}
	}
}