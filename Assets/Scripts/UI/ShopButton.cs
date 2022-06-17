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
		private Stat activeStat;

		private void Awake()
		{
			activeStat = Instantiate(stat);
		}
		

		public void UpdateUI()
		{
			titleText.text = activeStat.statName;
			priceText.text = activeStat.currentCost.ToString();
			levelText.text = "Level: " + activeStat.level;
		}

		public void Buy()
		{
			if (!CurrencyHandler.instance.RemoveMoney(stat.currentCost))
			{
				//todo: show not enough money
			}
			else
			{
				activeStat.Upgrade();
				UpdateUI();
			}
		}
	}
}