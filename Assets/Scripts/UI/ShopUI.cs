using System;
using System.Collections.Generic;
using System.Linq;
using StuartHeathTools;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ShopUI : UICanvas
	{
		[SerializeField] private List<GridLayoutGroup> grids;
		[SerializeField] private List<ShopVerticalButton> verticalButtons;
		[SerializeField] private TextMeshProUGUI currentCurrency;
		private GridLayoutGroup activeGrid;
		private List<Button> cachedButtons = new List<Button>();

		private void OnEnable()
		{
			GameManager.onStateChange += GameManagerOnonStateChange;
			CurrencyHandler.onCurrencyChanged += UpdateCurrency;
			ShopButton.OnPurchase += UpdateButtonsFromEvent;

		}

		private void OnDisable()
		{
			GameManager.onStateChange -= GameManagerOnonStateChange;
			CurrencyHandler.onCurrencyChanged -= UpdateCurrency;
			ShopButton.OnPurchase -= UpdateButtonsFromEvent;
		}

		private void Start()
		{
			cachedButtons = grids.SelectMany(grid => grid.GetComponentsInChildren<Button>()).ToList();
			Reset();
		}

		private void Reset()
		{
			Hide(0f);
			SelectManualWeapons();
			foreach (var b in cachedButtons)
			{
				b.gameObject.SetActive(true);
			}
		}

		public static void NextLevel()
		{
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.NewWave);
		}

		private void UpdateButtonsFromEvent() => UpdateButtonsDelay();
		public void SelectManualWeapons() => ShowGrid(GridTypes.ManualWeapons);
		public void SelectAutomaticWeapons() => ShowGrid(GridTypes.AutomaticWeapons);
		public void SelectDefense() => ShowGrid(GridTypes.Defense);
		public void SelectUtility() => ShowGrid(GridTypes.Utility);
		private void SetActiveGrid(GridLayoutGroup grid) => activeGrid = grid == null ? grids[0] : grid;
		private GridLayoutGroup GetActiveGrid() => activeGrid == null ? grids[0] : activeGrid;
		private void UpdateCurrency(long newCurrency) =>
			currentCurrency.text = "$" + Utility.FormatMoneyToKMB(newCurrency);

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.NewGame) Reset();
			if (state == GameState.Shop) Show(2f);
			else Hide(0f);
		}


		protected override void Show(float fadeTime=1f)
		{
			base.Show(fadeTime);
			Invoke(nameof(UpdateButtons),.05f);
			
		}

		private void UpdateButtonsDelay()
		{
			Invoke(nameof(UpdateButtons),.05f);

		}
		private void UpdateButtons()
		{
			var buttons = GetActiveGrid().GetComponentsInChildren<ShopButton>().ToList();
			foreach (var button in buttons)
			{
				button.UpdateUI();
			}
		}

		private void ShowGrid(GridTypes type)
		{
			if ((int) type >= grids.Count)
			{
				Debug.LogError("Not enough grids to show");
				return;
			}

			foreach (var grid in grids)
			{
				grid.gameObject.SetActive(false);
			}

			foreach (var button in verticalButtons)
			{
				button.Deselect();
			}

			verticalButtons[(int) type].Select();
			SetActiveGrid(grids[(int) type]);
			activeGrid.gameObject.SetActive(true);
			UpdateButtons();
		}
	}

	public enum GridTypes
	{
		ManualWeapons,
		AutomaticWeapons,
		Defense,
		Utility
	}
}