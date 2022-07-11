using System;
using TMPro;
using UnityEngine;
using Upgrades;
using StuartHeathTools;
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
		[SerializeField] protected TextMeshProUGUI currentValue;
		[SerializeField] protected TextMeshProUGUI upgradeAmount;
		[SerializeField] private GameObject valueComponent;
		public static event Action OnPurchase;

		public virtual void UpdateUI()
		{
			titleText.text = item.GetStatName();
			UpdatePriceText();
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

			UpdateValues();
			levelText.text = "Level: " + item.GetLevel();
			if (CurrencyHandler.instance.CanAfford(item.GetCurrentCost())) ShowPurchasable();
			else UnshowPurchasable();
		}
		protected void UpdateValues()
		{
			if (item.GetType() != typeof(Stat))
			{
				valueComponent.SetActive(false);
				return;
			}
			valueComponent.SetActive(true);
			Stat stat = (Stat) item;
			currentValue.text = stat.runTimeValue.ToString("0.0") + stat.GetChangeSymbol();
			upgradeAmount.text = ((stat.runTimeValue * stat.GetValueModifier())-stat.runTimeValue).ToString("0.0")+ stat.GetChangeSymbol();
		}

		protected void UpdatePriceText()
		{
			priceText.text = "$" + Utility.FormatMoneyToKMB(item.GetCurrentCost());
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
			SFXController.instance.PlayUIClick();

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