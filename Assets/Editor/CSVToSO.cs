//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.IO;
using Enemies;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	/// <summary>
	///CSVToSO full description
	/// </summary>
	public static class CSVToSO
	{
		private static string enemyCSVPath = "/Editor/CSVs/EnemyCSV.csv";

		[MenuItem("Planet Defense/Generate Enemies")]
		public static void GenerateEnemies()
		{
			string[] allLines = File.ReadAllLines(Application.dataPath + enemyCSVPath);
			Debug.Log(allLines[0]);
			for (int i = 1; i < allLines.Length; i++)
			{
				string[] splitData = allLines[i].Split(',');
				EnemyStats enemy = ScriptableObject.CreateInstance<EnemyStats>();
				try
				{
					if (string.IsNullOrEmpty(splitData[0])) continue;
					Debug.Log(splitData[0]);
					enemy.characterName = splitData[0];
					enemy.maxHealth = float.Parse(splitData[1]);
					enemy.movementSpeed = float.Parse(splitData[2]);
					enemy.impactDamage = float.Parse(splitData[3]);
					enemy.currencyValue = uint.Parse(splitData[4]);

					AssetDatabase.CreateAsset(enemy, $"Assets/Resources/SO/Enemies/{enemy.characterName}.asset");
				}
				catch (Exception e)
				{
					Debug.Log("Incorrect data passed " + e);
				}

				AssetDatabase.SaveAssets();
			}
		}
	}
}