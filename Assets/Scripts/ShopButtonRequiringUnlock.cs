using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;

public class ShopButtonRequiringUnlock : ShopButton
{
	[SerializeField] Unlockable unlockable;

	public override void UpdateUI()
	{
		titleText.text = stat.GetStatName();
		priceText.text = stat.GetCurrentCost().ToString();
		levelText.text = "Level: " + stat.GetLevel();
		if (CurrencyHandler.instance.CanAfford(stat.GetCurrentCost()) && unlockable.isUnlocked) ShowPurchasable();
		else UnshowPurchasable();
	}

	public virtual void Buy()
	{
		if (!unlockable.isUnlocked) return;
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