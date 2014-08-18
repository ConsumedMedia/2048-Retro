using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GooglePlayGames;

public class AndroidGamecenterProxy:GamecenterProxy
{

    protected override void _initIDs()
    {
        PlayGamesPlatform.DebugLogEnabled = false;
        PlayGamesPlatform.Activate();
        _leaderboardId = Settings.instance.leaderboardAndroid;
    }
        
}

