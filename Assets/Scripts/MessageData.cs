//
// Copyright (C) 2022 Stuart Heath. All rights reserved.
//

using UnityEngine;

/// <summary>
///MessageData full description
/// </summary>
[CreateAssetMenu(fileName = "NewMessage", menuName = "Message")]
public class MessageData : ScriptableObject
{
	public string sender;
	public string message;
}