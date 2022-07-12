using System;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies
{
	[CreateAssetMenu(fileName = "New Wave Container", menuName = "Waves/Wave Container")]
	public class WaveContainer : ScriptableObject
	{
		public List<WaveData> waves;

		private void OnValidate()
		{
			if (waves.Count == 0)
			{
				Logger.LogError("waves data missing");

			}
		}

		public WaveData GetWaveByIndex(int index) => index > waves.Count ? null : waves[index];


		public int GetIndexByWave(WaveData data)
		{
			if (data == null) return -1;
			return waves.IndexOf(data);
		}

		public bool IsLastWave(int currentIndex) => currentIndex >= waves.Count;
	}
}