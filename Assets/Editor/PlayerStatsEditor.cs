//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using System.Collections.Generic;
using System.Linq;
using Editor.ScriptCreation;
using StuartHeathToolsEditor;
using UnityEditor;
using UnityEngine;
using Upgrades;

namespace Editor
{
	/// <summary>
	///PlayerStatsEditor full description
	/// </summary>
	public class PlayerStatsEditor : EditorWindow
	{
		private List<Stat> stats = new List<Stat>();
		private List<Unlockable> unlockables = new List<Unlockable>();

		[MenuItem("Planet Defense/Player Data", false, 0)]
		public static void ShowWindow()
		{
			GetWindow<PlayerStatsEditor>("Player Data");
		}

		private void OnGUI()
		{
			stats = (Resources.FindObjectsOfTypeAll(typeof(Stat)) as Stat[]).ToList();
			unlockables = (Resources.FindObjectsOfTypeAll(typeof(Unlockable)) as Unlockable[]).ToList();
			GUIStyle s = new GUIStyle(GUI.skin.label) {alignment = TextAnchor.MiddleCenter};

			EditorGUILayout.Space();
			EditorGUIUtility.labelWidth = 70;

			for (int i = 0; i < stats.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				EditorGUILayout.ObjectField(stats[i], typeof(Stat));
				EditorGUILayout.LabelField("Value: ", stats[i].GetCurrentValue().ToString(),s);
				EditorGUILayout.LabelField("Level: ", stats[i].GetLevel().ToString(),s);
				EditorGUILayout.LabelField("Cost: ", stats[i].GetCurrentCost().ToString(),s);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();
			UtilityEditor.LineBreak();
			for (int i = 0; i < unlockables.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				EditorGUILayout.Space();
				EditorGUILayout.ObjectField(stats[i], typeof(Stat));
				GUIStyle guiStyle = new GUIStyle(GUI.skin.label);


				guiStyle.normal.textColor = unlockables[i].GetIsUnlocked() ? Color.green : Color.red;

				EditorGUILayout.LabelField("Is unlocked?: ", unlockables[i].GetIsUnlocked().ToString(),guiStyle);
				EditorGUILayout.LabelField("Cost: ", unlockables[i].GetCurrentCost().ToString(),s);
				EditorGUILayout.EndHorizontal();
			}

			EditorGUILayout.Space();
			UtilityEditor.LineBreak();
		}

		private void CreateCachedEditor(object objectReferenceValue, object o, ref object editor)
		{
			throw new NotImplementedException();
		}
	}
}