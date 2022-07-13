using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Enemies;
using UnityEngine;
using UnityEditor;

namespace Editor
{
	public static class CSVTOStorySO
	{
		private static string path = "/Editor/CSVs/Story.csv";
		private static LevelMessageContainer lmc;

		[MenuItem("Planet Defense/Generate Story")]
		public static void GenerateStoryData()
		{
			try
			{
				Logger.LogError("updating Story container........");
				ClearData();
				lmc = GetContainer();
				if (lmc == null) throw new Exception("Unable to get level message container");
				lmc.levelMessageData.Clear();
				ParseMessages(path);
				Logger.LogWithColor("Loaded story data", Color.green);
			}
			catch (Exception e)
			{
				Debug.LogError("Failed to load data: " + e);
				throw;
			}
		}

		private static void ParseMessages(string path)
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

				LevelMessageData lmd = ScriptableObject.CreateInstance<LevelMessageData>();
				lmd.messageData = new List<MessageData>();
				
				for (int j = 1; j < splitLine.Length; j++)
				{
					if (string.IsNullOrEmpty(splitLine[j])) continue;
					var messageData = ScriptableObject.CreateInstance<MessageData>();
					messageData.sender = splitLine[j];
					j++;
					messageData.message = splitLine[j];
					AssetDatabase.CreateAsset(messageData, $"Assets/Resources/SO/Messages/messageData{i}-{j-1}.asset");
					lmd.messageData.Add(messageData);
				}
				lmd.level = int.Parse(splitLine[0]);
				if (lmd.level != -10) lmc.levelMessageData.Add(lmd);
				else lmc.pregameLevelMessageData = lmd;
				
				AssetDatabase.CreateAsset(lmd, $"Assets/Resources/SO/Messages/levelMessageData{i}.asset");

			}
			EditorUtility.SetDirty(lmc);
			AssetDatabase.SaveAssets();
		}

		private static LevelMessageContainer GetContainer()
		{
			var data = ((Resources.FindObjectsOfTypeAll(typeof(LevelMessageContainer)) as LevelMessageContainer[]) ??
			            Array.Empty<LevelMessageContainer>())
				.ToList();
			if (data.Count == 0) Logger.LogError("message container data count is 0");
			return data[0];
		}


		private static void ClearData()
		{
			foreach (var e in Resources.LoadAll<MessageData>("so"))
			{
				Debug.Log(e);
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(e));
			}

			foreach (var e in Resources.LoadAll<LevelMessageData>("so"))
			{
				Debug.Log(e);
				AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(e));
			}

			AssetDatabase.SaveAssets();
		}
	}
}