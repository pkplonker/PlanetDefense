 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //
using UnityEngine;
using UnityEditor;

    /// <summary>
    ///AutoPlayWinEditor full description
    /// </summary>
    [CustomEditor(typeof(AutoPlayWin))]
public class AutoPlayWinEditor : UnityEditor.Editor
{
   public override void OnInspectorGUI()
   {
	   if (GUILayout.Button("AutoPlay"))
	   {
		   var t = (AutoPlayWin) target;
		   t.AutoPlay();
	   }
      base.OnInspectorGUI();
   }
}
