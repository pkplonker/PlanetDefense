using System;
using TMPro;
using UnityEngine;

namespace UI
{
	public class ShopButton : MonoBehaviour
	{
		[SerializeField] protected TextMeshProUGUI titleText;
		[SerializeField] protected TextMeshProUGUI priceText;
		[SerializeField] protected TextMeshProUGUI levelText;
		[SerializeField] protected Stat stat;
		[SerializeField] protected Color canAffordColor;
		[SerializeField] protected Color cannotAffordColor;


		public virtual void UpdateUI()
		{
			titleText.text = stat.GetStatName();
			priceText.text = stat.GetCurrentCost().ToString();
			levelText.text = "Level: " + stat.GetLevel();
			if (CurrencyHandler.instance.CanAfford(stat.GetCurrentCost())) ShowPurchasable();
			else UnshowPurchasable();
		}

		protected virtual void UnshowPurchasable()
		{
			priceText.color = cannotAffordColor;
			titleText.color = cannotAffordColor;
			levelText.color = cannotAffordColor;
		}

		protected virtual void ShowPurchasable()
		{
			priceText.color = canAffordColor;
			titleText.color = canAffordColor;
			levelText.color = canAffordColor;
		}

		public virtual void Buy()
		{
			if (!CurrencyHandler.instance.RemoveMoney(stat.GetCurrentCost()))
			{
				//todo: show not enough money
			}
			else
			{
				stat.Buy();
				UpdateUI();
			}
		}
	}
}