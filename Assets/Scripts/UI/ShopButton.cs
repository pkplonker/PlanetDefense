using System;
using TMPro;
using UnityEngine;
using Upgrades;

namespace UI
{
	public class ShopButton : MonoBehaviour
	{
		[SerializeField] protected TextMeshProUGUI titleText;
		[SerializeField] protected TextMeshProUGUI priceText;
		[SerializeField] protected TextMeshProUGUI levelText;
		[SerializeField] public Purchaseable item;
		[SerializeField] protected Color canAffordColor;
		[SerializeField] protected Color cannotAffordColor;
		public static event Action OnPurchase;

		public virtual void UpdateUI()
		{
			titleText.text = item.GetStatName();
			priceText.text = item.GetCurrentCost().ToString();
			if (item.GetType() == typeof(Stat))
			{
				levelText.enabled = true;
				levelText.text = "Level " + ((Stat) item).GetLevel();
			}
			else
			{
				levelText.enabled = false;
				levelText.text = "";
			}

			levelText.text = "Level: " + item.GetLevel();
			if (CurrencyHandler.instance.CanAfford(item.GetCurrentCost())) ShowPurchasable();
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
			if (!CurrencyHandler.instance.RemoveMoney(item.GetCurrentCost()))
			{
				//todo: show not enough money
			}
			else
			{
				item.Buy();
				if (item.GetIsOneTimePurchase()) HandleOneTimePurchase();
				UpdateUI();
			}

			OnPurchase?.Invoke();
		}

		protected virtual void HandleOneTimePurchase() => gameObject.SetActive(false);
	}
}