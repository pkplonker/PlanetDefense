//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemies;
using StuartHeathTools;
using UnityEditor;
using UnityEngine;
using Upgrades;

namespace Editor
{
	/// <summary>
	///CSVToSO full description
	/// </summary>
	public static class CsvToSo
	{
		private static string enemyCSVPath = "/Editor/CSVs/EnemyCSV.csv";
		private static string waveCSVPath = "/Editor/CSVs/WaveCSV.csv";

		[MenuItem("Planet Defense/Generate Enemies")]
		public static void GenerateEnemyData()
		{
			try
			{
				Logger.LogError("updating wave container........");

				ClearData();
				GenerateEnemies(enemyCSVPath);
				GenerateWaves(waveCSVPath);
				PopulateContainer();
			}
			catch (Exception e)
			{
				Debug.LogError("Failed to load data: " + e);
				throw;
			}

			Debug.Log("Loaded Data".WithColor(Color.green));
			AssetDatabase.SaveAssets();

		}

		private static void ClearData()
		{
			foreach (var e in Resources.LoadAll<EnemyStats>("so") )
			{
				Debug.Log(e);
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(e));
			}
			foreach (var e in Resources.LoadAll<WaveData>("so") )
			{
				Debug.Log(e);
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(e));
			}
			AssetDatabase.SaveAssets();

		}

		private static void PopulateContainer()
		{
			var data = ((Resources.FindObjectsOfTypeAll(typeof(WaveData)) as WaveData[]) ?? Array.Empty<WaveData>())
				.ToList();
			if (data.Count == 0) Logger.LogError("Populate container data count is 0");
			for (int i = data.Count - 1; i >= 0; i--)
			{
				if (data[i].levelIndex == 0) data.Remove(data[i]);
			}

			var container = Resources.FindObjectsOfTypeAll(typeof(WaveContainer));
			var waveContainer = container[0] as WaveContainer;
			var orderedEnumerable = data.OrderBy(x => x.levelIndex);

			if (waveContainer != null)
			{
				waveContainer.waves = new List<WaveData>();
				waveContainer.waves = orderedEnumerable.ToList();
			}
			else Debug.LogError("Wave Container missing");
			EditorUtility.SetDirty(waveContainer);
			AssetDatabase.SaveAssets();

		}

		private static void GenerateEnemies(string path)
		{
			string[] allLines = File.ReadAllLines(Application.dataPath + path);
			Debug.Log(allLines[0]);
			for (int i = 2; i < allLines.Length; i++)
			{
				string[] splitData = allLines[i].Split(',');
				EnemyStats enemy = ScriptableObject.CreateInstance<EnemyStats>();
				try
				{
					if (string.IsNullOrEmpty(splitData[0])) continue;
					enemy.characterName = splitData[0];
					enemy.maxHealth = float.Parse(splitData[1]);
					enemy.movementSpeed = float.Parse(splitData[2]);
					enemy.impactDamage = float.Parse(splitData[3]);
					enemy.currencyValue = uint.Parse(splitData[4]);
					enemy.projectileDataPath = splitData[5];
					enemy.spritePath = splitData[6];
					if (enemy.characterName == "Rock 1")
					{
						Debug.Log(splitData[7]);
						Debug.Log(float.Parse(splitData[7]));

					}
					
					enemy.color = new Color(float.Parse(splitData[7]), float.Parse(splitData[8]), float.Parse(splitData[9]), float.Parse(splitData[10]));
					
					enemy.team = Stats.Team.Enemy;
					AssetDatabase.CreateAsset(enemy, $"Assets/Resources/SO/Enemies/{enemy.characterName}.asset");
				}
				catch (Exception e)
				{
					Debug.Log("Incorrect data passed " + e);
				}

				AssetDatabase.SaveAssets();
			}
			AssetDatabase.SaveAssets();

		}

		private static void GenerateWaves(string path)
		{
			var allLines = File.ReadAllLines(Application.dataPath + path).ToList();
			foreach (var line in allLines.Where(string.IsNullOrEmpty))
			{
				allLines.Remove(line);
			}

			Debug.Log(allLines[0]);
			for (int i = 1; i < allLines.Count; i++)
			{
				string[] splitLine = allLines[i].Split(',');
				WaveData waveData = ScriptableObject.CreateInstance<WaveData>();
				waveData.spawns = new List<Spawn>();
				if (string.IsNullOrEmpty(splitLine[0])) continue;
				waveData.name = splitLine[0];
				waveData.levelIndex = int.Parse(splitLine[1]);

				for (int j = 2; j < splitLine.Length; j++)
				{
					if (string.IsNullOrEmpty(splitLine[j])) continue;
					var spawn = new Spawn();
					
					spawn.SetEnemyStats(Resources.Load(splitLine[j]) as EnemyStats);
					if(spawn.enemyStats==null) Debug.Log("Failed to generate stats, wave "+ waveData.levelIndex + ", Enemy: "+((j-1)/2));
					j++;
					spawn.nextMobDelay = float.Parse(splitLine[j]);
					waveData.spawns.Add(spawn);
				}

				AssetDatabase.CreateAsset(waveData, $"Assets/Resources/SO/Waves/{waveData.name}.asset");
			}
			AssetDatabase.SaveAssets();
		}
		
	}
}