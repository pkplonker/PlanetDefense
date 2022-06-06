using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class WaveUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI waveText;
    [SerializeField] private TextMeshProUGUI mobText;

    private void OnEnable()
    {
        WaveSpawner.OnCurrentWaveChange += ChangeWave;
        WaveSpawner.OnMobCountChange += ChangeMob;

    }

    private void ChangeMob(int current, int total)
    {
        mobText.text = current + "/" +total;
    }

    private void OnDisable()
    {
        WaveSpawner.OnCurrentWaveChange -= ChangeWave;
        WaveSpawner.OnMobCountChange -= ChangeMob;
    }

    private void ChangeWave(int currentWave)
    {
        waveText.text = "Wave: " + (currentWave+1) + " ("+currentWave+")";
    }
}
