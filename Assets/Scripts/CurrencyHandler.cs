using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CurrencyHandler : MonoBehaviour
{
   [SerializeField] private PlayerStats stats;
   [SerializeField]private uint startingCurrency = 0;
   [SerializeField] private readonly uint DEFAULT_CURRENCY;
   public static event Action<uint> onCurrencyChanged;

   private void Awake()
   {
      stats.currency = DEFAULT_CURRENCY;
   }

   private void Start()
   {
      onCurrencyChanged?.Invoke(stats.currency);
   }

   private void OnEnable()
   {
      GameManager.onStateChange += HandleGameStateChange;
      EnemySpawner.OnEnemyDeath += EnemyDeath;
   }

   private void OnDisable()
   {
      GameManager.onStateChange -= HandleGameStateChange;
      EnemySpawner.OnEnemyDeath -= EnemyDeath;

   }

   private void HandleGameStateChange(GameState state)
   {
      if (state == GameState.GameOver)
      {
         stats.currency = startingCurrency;
         onCurrencyChanged?.Invoke(stats.currency);

      }
   }

   public void AddMoney(uint amount)
   {
      stats.currency += amount;
      onCurrencyChanged?.Invoke(stats.currency);
   }

   public bool RemoveMoney(uint amount)
   {
      if (stats.currency < amount) return false;
      stats.currency -= amount;
      onCurrencyChanged?.Invoke(stats.currency);
      return true;

   }

   private void EnemyDeath(EnemyStats stats)
   {
      AddMoney(stats.value);
   }
}
