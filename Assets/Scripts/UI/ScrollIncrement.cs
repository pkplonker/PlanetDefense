 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System;
 using UnityEngine;
 using UnityEngine.Rendering;
 using UnityEngine.UI;

 namespace UI
 {
	 /// <summary>
	 ///ScrollIncrement full description
	 /// </summary>
	 [RequireComponent(typeof(DebugUI.Button))]

	 public class ScrollIncrement : MonoBehaviour
	 {
		 [SerializeField] private Scrollbar Target;
		 [SerializeField] private Button TheOtherButton;
		 [SerializeField] private float Step = 0.1f;
 
		 public void Increment()
		 {
			 if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
			 Target.value = Mathf.Clamp(Target.value + Step, 0, 1);
			 GetComponent<Button>().interactable = Target.value != 1;
			 TheOtherButton.interactable = true;
		 }
 
		 public void Decrement()
		 {
			 if (Target == null || TheOtherButton == null) throw new Exception("Setup ScrollbarIncrementer first!");
			 Target.value = Mathf.Clamp(Target.value - Step, 0, 1);
			 GetComponent<Button>().interactable = Target.value != 0;;
			 TheOtherButton.interactable = true;
		 }
	 }
 }
