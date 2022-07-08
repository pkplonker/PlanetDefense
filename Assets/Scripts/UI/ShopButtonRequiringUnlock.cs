using UnityEngine;
using Upgrades;

namespace UI
{
	public class ShopButtonRequiringUnlock : ShopButton
	{
		[SerializeField] Unlockable unlockable;

		public override void UpdateUI()
		{
			UpdateValues();
			titleText.text = item.GetStatName();
			UpdatePriceText();
			levelText.text = "Level: " + item.GetLevel();
			if (CurrencyHandler.instance.CanAfford(item.GetCurrentCost()) && unlockable.GetIsUnlocked())
				ShowPurchasable();
			else UnshowPurchasable();
		}

		public override void Buy()
		{
			if (!unlockable.GetIsUnlocked()) return;
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
		}
	}
}