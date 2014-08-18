using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine.SocialPlatforms.GameCenter;

public class IOSGamecenterProxy:GamecenterProxy
{
    protected override void _initIDs()
    {
        _leaderboardId = Settings.instance.leaderboardIOS;
        GameCenterPlatform.ShowDefaultAchievementCompletionBanner(false);
    }
        
    public override void showLeaderboard()
    {
        if (_logged)
        {
            GameCenterPlatform.ShowLeaderboardUI(_leaderboardId, UnityEngine.SocialPlatforms.TimeScope.Week);
        }
        else
        {
            _needShowLeaderboards = true;
            _authenticate();
        }
    }
}

