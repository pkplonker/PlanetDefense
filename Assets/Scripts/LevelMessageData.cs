 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System.Collections.Generic;
 using UnityEngine;

    /// <summary>
    ///LevelMessageData full description
    /// </summary>
    	[CreateAssetMenu(fileName = "LevelMessageData",menuName = "Level Message Data")]

public class LevelMessageData : ScriptableObject
    {
	    public int level;
	    public List<MessageData> messageData;
    }
