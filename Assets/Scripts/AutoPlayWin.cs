using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UI;
using UnityEngine;
using Upgrades;

public class AutoPlayWin : MonoBehaviour
{
	[SerializeField] private Purchaseable[] upgrades;
	private bool autoOn = false;

	private void Awake()
	{
#if !UNITY_EDITOR
                Destroy(gameObject);

#endif
	}

	public void AutoPlay()
	{
		autoOn = true;
		GameManager.Instance.ChangeState(GameState.NewGame);
		Purchase();
		var x = FindObjectOfType<SpeedButton>();
		x.SetSpeedMultiplier(GameSpeed.SixteenTimes);
		SetMobDelayToZero();
	}

	private static void SetMobDelayToZero()
	{
		var waveData = Resources.LoadAll<WaveData>("SO").ToList();
		foreach (var w in waveData)
		{
			foreach (var spawn in w.spawns)
			{
				spawn.nextMobDelay = 0;
			}
		}
	}

	private void Update()
	{
		if (!autoOn) return;
		var state = GameManager.GetCurrentState();
		if (state == GameState.Shop)
		{
			ShopUI.NextLevel();
		}

		if (state == GameState.Complete || state == GameState.GameOver)
		{
			autoOn = false;
		}
	}

	private void Purchase()
	{
		CurrencyHandler.instance.AddMoney(long.MaxValue);
		foreach (var u in upgrades)
		{
			for (int i = 0; i < 30; i++)
			{
				if (u.GetIsOneTimePurchase())
				{
					u.Buy();
					break;
				}

				u.Buy();
				CurrencyHandler.instance.AddMoney(long.MaxValue);
			}
		}
	}
}