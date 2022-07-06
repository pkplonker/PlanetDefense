//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using System;
using StuartHeathTools;

namespace UI
{
	/// <summary>
	///Settings full description
	/// </summary>
	public class Settings : CanvasGroupBase
	{
		public static Settings Instance;

		private void Awake()
		{
			if (Instance == this) return;

			if (Instance == null)
			{
				Instance = this;
				DontDestroyOnLoad(gameObject);
				return;
			}

			Destroy(gameObject);
		}

		private void Start() => HideUI();
		public void Back() => HideUI();

		public void Show() => ShowUI(0.5f);
	}
}