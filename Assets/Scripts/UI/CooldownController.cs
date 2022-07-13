using System;
using PlayerScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Upgrades;

namespace UI
{
	public class CooldownController : MonoBehaviour
	{
		[SerializeField] private Slider manual;
		[SerializeField] private Slider auto;
		[SerializeField] private TextMeshProUGUI manualValueText;
		[SerializeField] private TextMeshProUGUI autoValueText;
		[SerializeField] private Unlockable autoShootUnlock;

		private void Start() => Reset();
		private void Reset() => auto.gameObject.SetActive(false);
		private void UnlockAuto() => auto.gameObject.SetActive(true);


		private void AutoCooldownUpdate(float timeRemaining, float targetTime)
		{
			if (timeRemaining < 0) timeRemaining = 0;
			autoValueText.text = timeRemaining.ToString("n1") + "s";
			auto.value = (timeRemaining / targetTime) * 1000;
		}

		private void ManualCooldownUpdate(float timeRemaining, float targetTime)
		{
			if (timeRemaining < 0) timeRemaining = 0;
			manualValueText.text = timeRemaining.ToString("n1") + "s";
			manual.value = (timeRemaining / targetTime) * 1000;
		}

		private void OnEnable()
		{
			PlayerCombatManager.OnACoolDownUpdate += AutoCooldownUpdate;
			autoShootUnlock.OnPurchase += UnlockAuto;
			PlayerManualShooter.OnMCoolDownUpdate += ManualCooldownUpdate;
			GameManager.onStateChange += StateChange;
		}

		private void OnDisable()
		{
			PlayerCombatManager.OnACoolDownUpdate -= AutoCooldownUpdate;
			autoShootUnlock.OnPurchase -= UnlockAuto;
			PlayerManualShooter.OnMCoolDownUpdate -= ManualCooldownUpdate;
			GameManager.onStateChange -= StateChange;
		}

		private void StateChange(GameState state)
		{
			if (state == GameState.NewGame) Reset();
		}
	}
}