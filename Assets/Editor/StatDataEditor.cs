using UnityEditor;
using UnityEngine;
using Upgrades;

namespace Editor
{
	[CustomEditor(typeof(Stat))]
	public class StatDataEditor : UnityEditor.Editor
	{
		private UnityEditor.Editor editor;
		public override void OnInspectorGUI()
		{
			var t = (Stat) target;

			DrawDefaultInspector();
			EditorGUILayout.Space();
			LineBreak();
			EditorGUILayout.LabelField("Runtime Data", EditorStyles.boldLabel);
			EditorGUILayout.LabelField("Value: ", t.GetCurrentValue().ToString());
			EditorGUILayout.LabelField("Level: ", t.GetLevel().ToString());
			EditorGUILayout.LabelField("Cost: ", t.GetCurrentCost().ToString());
			

			EditorGUILayout.Space();
		}

		/// <summary>
		///   <para>Produces a line separator for custom editor</para>
		/// </summary>
		private void LineBreak(int height = 1)
		{
			Rect rect = EditorGUILayout.GetControlRect(false, height);
			rect.height = height;
			EditorGUI.DrawRect(rect, new Color(0.5f, 0.5f, 0.5f, 1));
		}
	}
}