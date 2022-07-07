using System;
using UnityEngine;

namespace Upgrades
{
	[CreateAssetMenu(fileName = "New Stat", menuName = "Unlockable/New Stat")]
	public class Stat : Purchaseable
	{
		[SerializeField] private float value;
		[Range(0, 5)] [SerializeField] private float valueModifier = 1.1f;
		[SerializeField] private long currentCost;
		[Range(1, 5)] [SerializeField] private uint costMultiplier;
		[SerializeField] private int level;
		[SerializeField] private string statName;
		[SerializeField] private bool isOneTimePurchase = false;
		[SerializeField] private string changeSymbol;
		public float runTimeValue { get; private set; }
		public long runTimeCurrentCost { get; private set; }
		public int runTimeLevel { get; private set; }
		public event Action<float, float> OnValueChanged;
		public float GetValueModifier() => valueModifier;
		public string GetChangeSymbol() => changeSymbol;
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

		public override long GetCurrentCost() => runTimeCurrentCost;
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