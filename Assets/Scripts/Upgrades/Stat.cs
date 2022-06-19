using System;
using UnityEngine;

namespace Upgrades
{
	[CreateAssetMenu(fileName = "New Stat", menuName = "Unlockable/New Stat")]
	public class Stat : Purchaseable
	{
		[SerializeField] private float value;
		[Range(0, 5)] [SerializeField] private float valueModifier = 1.1f;
		[SerializeField] private ulong currentCost;
		[Range(1, 5)] [SerializeField] private uint costMultiplier;
		[SerializeField] private int level;
		[SerializeField] private string statName;
		[SerializeField] private bool isOneTimePurchase = false;
		private float runTimeValue;
		private ulong runTimeCurrentCost;
		private int runTimeLevel;
		public event Action<float, float> OnValueChanged;

		private void OnEnable()
		{
			ResetData();
			GameManager.onStateChange += OnStateChange;
		}

		private void OnStateChange(GameState state)
		{
			if (state == GameState.NewGame) ResetData();
		}

		private void ResetData()
		{
			runTimeValue = value;
			runTimeLevel = level;
			runTimeCurrentCost = currentCost;
		}

		public override ulong GetCurrentCost() => runTimeCurrentCost;
		public override string GetStatName() => statName;
		public override string GetLevel() => runTimeLevel.ToString();
		public override bool GetIsOneTimePurchase() => isOneTimePurchase;
		public float GetCurrentValue() => runTimeValue;

		public override void Buy()
		{
			var cachedValue = runTimeValue;
			runTimeValue *= valueModifier;
			OnValueChanged?.Invoke(cachedValue, runTimeValue);
			runTimeCurrentCost *= costMultiplier;
			runTimeLevel++;
		}
	}
}