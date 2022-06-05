using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
   [SerializeField] private PlayerStats stats;
   [SerializeField]private uint startingCurrency = 0;
   public event Action<uint> onCurrencyChanged;

   private void Start()
   {
      onCurrencyChanged?.Invoke(stats.currency);
   }

   private void OnEnable()
   {
      GameManager.onStateChange += HandleGameStateChange;
   }

   private void OnDisable()
   {
      GameManager.onStateChange -= HandleGameStateChange;

   }

   private void HandleGameStateChange(GameState state)
   {
      if (state == GameState.GameOver)
      {
         stats.currency = startingCurrency;
      }
   }

   public void AddMoney(uint amount)
   {
      stats.currency += amount;
      onCurrencyChanged?.Invoke(stats.currency);
   }

   public bool RemoveMoney(uint amount)
   {
      if (stats.currency >= amount)
      {
         stats.currency -= amount;
         onCurrencyChanged?.Invoke(stats.currency);
         return true;
      }

      return false;

   }
}
