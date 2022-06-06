using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class PlayerCurrencyUI : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI tmp;

		private void OnEnable()
		{
			CurrencyHandler.onCurrencyChanged += UpdateUI;
		}

		private void OnDisable()
		{
			CurrencyHandler.onCurrencyChanged -= UpdateUI;
		}
		

		private void UpdateUI(uint amount)
		{
			tmp.text = "$" + amount;
		}
	}
}