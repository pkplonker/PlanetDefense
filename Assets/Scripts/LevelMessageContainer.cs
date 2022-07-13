 //
 // Copyright (C) 2022 Stuart Heath. All rights reserved.
 //

 using System.Collections.Generic;
 using UnityEngine;

    /// <summary>
    ///LevelMessageContainer full description
    /// </summary>
    	[CreateAssetMenu(fileName = "Message Container",menuName = "MessageContainer")]

public class LevelMessageContainer : ScriptableObject
    {
	    public LevelMessageData pregameLevelMessageData;

	    public List<LevelMessageData> levelMessageData;
    }
