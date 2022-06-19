using UI;
using UnityEngine;

public class ShopButtonRequiringUnlock : ShopButton
{
	[SerializeField] Unlockable unlockable;

	public override void UpdateUI()
	{
		titleText.text = item.GetStatName();
		priceText.text = item.GetCurrentCost().ToString();
		levelText.text = "Level: " + item.GetLevel();
		if (CurrencyHandler.instance.CanAfford(item.GetCurrentCost()) && unlockable.GetIsUnlocked()) ShowPurchasable();
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

	private void HandleOneTimePurchase()=>Destroy(gameObject);
	
}