using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class ShopButton : MonoBehaviour
	{
		[SerializeField] private TextMeshProUGUI titleText;
		[SerializeField] private TextMeshProUGUI priceText;
		[SerializeField] private TextMeshProUGUI levelText;
		[SerializeField] private Stat stat;
		[SerializeField] private Color canAffordColor;
		[SerializeField] private Color cannotAffordColor;

	


		public void UpdateUI()
		{
			titleText.text = stat.GetStatName();
			priceText.text = stat.GetCurrentCost().ToString();
			levelText.text = "Level: " + stat.GetLevel();
			if (CurrencyHandler.instance.CanAfford(stat.GetCurrentCost()))
			{
				priceText.color = canAffordColor;
				titleText.color = canAffordColor;
				levelText.color = canAffordColor;
			}
			else
			{
				priceText.color = cannotAffordColor;
				titleText.color = cannotAffordColor;
				levelText.color = cannotAffordColor;
			}
		}

		public void Buy()
		{
			if (!CurrencyHandler.instance.RemoveMoney(stat.GetCurrentCost()))
			{
				//todo: show not enough money
			}
			else
			{
				stat.Upgrade();
				UpdateUI();
			}
		}
	}
}