using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Wave Container", menuName = "Waves/Wave Container")]

public class WaveContainer : ScriptableObject
{
    public List<WaveData> waves;

    public WaveData GetWaveByIndex(int index)=>index > waves.Count ? null : waves[index];
    

    public int GetIndexByWave(WaveData data)
    {
	    if (data == null) return -1;
	    return waves.IndexOf(data);
    }

    public bool IsLastWave(int currentIndex) => currentIndex >= waves.Count;


    
}
