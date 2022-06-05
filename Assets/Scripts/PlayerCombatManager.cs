using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatManager : MonoBehaviour
{
    private PlayerStats stats;

    private void Awake()
    {
        stats = (PlayerStats)GetComponent<Player>().GetStats();
    }
}
