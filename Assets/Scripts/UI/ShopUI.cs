using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
	public class ShopUI : UICanvas
	{
		[SerializeField] private List<GridLayoutGroup> grids;
		[SerializeField] private TextMeshProUGUI currentCurrency;
		private GridLayoutGroup activeGrid;
		[SerializeField] private List<ShopVerticalButton> verticalButtons;

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
			Hide();
			SelectManualWeapons();
		}

		public static void NextLevel()
		{
			SFXController.instance.PlayUIClick();
			GameManager.ChangeState(GameState.NewWave);
		}

		private void UpdateButtonsFromEvent() => UpdateButtons(GetActiveGrid());
		public void SelectManualWeapons() => ShowGrid(GridTypes.ManualWeapons);
		public void SelectAutomaticWeapons() => ShowGrid(GridTypes.AutomaticWeapons);
		public void SelectDefense() => ShowGrid(GridTypes.Defense);
		public void SelectUtility() => ShowGrid(GridTypes.Utility);
		private void SetActiveGrid(GridLayoutGroup grid) => activeGrid = grid == null ? grids[0] : grid;
		private GridLayoutGroup GetActiveGrid() => activeGrid == null ? grids[0] : activeGrid;

		private void GameManagerOnonStateChange(GameState state)
		{
			if (state == GameState.Shop) Show();
			else Hide();
		}

		private void UpdateCurrency(ulong newCurrency)
		{
			currentCurrency.text = "£" + newCurrency;
		}

		protected override void Show()
		{
			base.Show();
			UpdateButtons(GetActiveGrid());
		}


		private void UpdateButtons(GridLayoutGroup activeGrid)
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
			UpdateButtons(activeGrid);
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