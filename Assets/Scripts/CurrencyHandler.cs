using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
   [SerializeField] private PlayerStats stats;
   public event Action<uint> onCurrencyChanged;

   private void Start()
   {
      onCurrencyChanged?.Invoke(stats.currency);
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
