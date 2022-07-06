//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

#if UNITY_EDITOR

using Editor.ScriptCreation;
using StuartHeathToolsEditor;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	/// <summary>
	///Level Balance
	/// </summary>
	public class LevelBalanceSupport : EditorWindow
	{
		private static WaveContainer wc;

		[MenuItem("Planet Defense/Level Balance", false, 0)]
		public static void ShowWindow()
		{
			GetWindow<LevelBalanceSupport>("Level Balance");
		}

		private void OnGUI()
		{
			wc = Resources.FindObjectsOfTypeAll(typeof(WaveContainer))[0] as WaveContainer;
			if (wc == null) Debug.LogError("missing wavecontainer");
			var style = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

			GUILayout.BeginHorizontal();
			EditorGUILayout.Space();
			Gold(style);
			EditorGUILayout.Space();
			UtilityEditor.LineBreak();
			HP(style);
			EditorGUILayout.Space();
			UtilityEditor.LineBreak();
			GoldPerHP(style);
			GUILayout.EndHorizontal();
			EditorGUILayout.Space();
			UtilityEditor.LineBreak();
		}

		private static void Gold(GUIStyle style)
		{
			GUILayout.BeginVertical();
			GUILayout.Label("Money Earned", style);
			foreach (var wave in wc.waves)
			{
				long count = 0;
				foreach (var spawn in wave.spawns)
				{
					count += spawn.enemyStats.currencyValue;
				}

				GUILayout.Label("Wave: " + wave.levelIndex + "= " + count, style);
			}

			GUILayout.EndVertical();
		}

		private static void HP(GUIStyle style)
		{
			GUILayout.BeginVertical();

			GUILayout.Label("Combined HP", style);
			foreach (var wave in wc.waves)
			{
				float count = 0;
				foreach (var spawn in wave.spawns)
				{
					count += spawn.enemyStats.maxHealth;
				}

				GUILayout.Label("Wave: " + wave.levelIndex + "= " + count, style);
			}

			GUILayout.EndVertical();
		}

		private static void GoldPerHP(GUIStyle style)
		{
			GUILayout.BeginVertical();

			GUILayout.Label("Gold per HP", style);
			foreach (var wave in wc.waves)
			{
				float hp = 0;
				long gold = 0;
				foreach (var spawn in wave.spawns)
				{
					hp += spawn.enemyStats.maxHealth;
					gold += spawn.enemyStats.currencyValue;
				}

				GUILayout.Label("Wave: " + wave.levelIndex + "= " + hp / gold, style);
			}

			GUILayout.EndVertical();
		}
	}
}
#endif