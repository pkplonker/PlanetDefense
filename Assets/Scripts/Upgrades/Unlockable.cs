using System;
using UnityEngine;

namespace Upgrades
{
	[CreateAssetMenu(fileName = "New Unlockable", menuName = "Unlockable/New unlockable")]
	public class Unlockable : Purchaseable
	{
		[SerializeField] private string statName;
		[SerializeField] private bool isOneTimePurchase;

		[SerializeField] private bool isUnlocked;
		public event Action OnPurchase;
		public long price;

		public override string GetStatName() => statName;

		public override void Buy()
		{
			isUnlocked = true;
			OnPurchase?.Invoke();
		}

		public override long GetCurrentCost() => price;
		public override bool GetIsOneTimePurchase() => isOneTimePurchase;
		public bool GetIsUnlocked() => isUnlocked;
		public override string GetLevel() => "";

		private void ResetData() => isUnlocked = false;

		private void OnEnable()
		{
			ResetData();
			GameManager.onStateChange += OnStateChange;
		}

		private void OnStateChange(GameState state)
		{
			if (state == GameState.NewGame) ResetData();
		}
	}
}