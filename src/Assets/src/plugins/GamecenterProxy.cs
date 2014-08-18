using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using GooglePlayGames;
using UnityEngine.SocialPlatforms;
using UnityEngine.SocialPlatforms.GameCenter;

public class GamecenterProxy
{
    protected string _leaderboardId;

    protected bool _inited = false;
    protected bool _logged = false;
    protected bool _needShowLeaderboards = false;

    public void init()
    {
        if (_inited)
            return;

        _inited = true;

        _initIDs();

        #if UNITY_IPHONE || UNITY_EDITOR
        _authenticate();
        #endif
    }

    protected void _authenticate()
    {
        Social.localUser.Authenticate (success => {
            if (success) 
            {
                UserData.gamecenterLogged = true;
                _logged = true;

                sendBestScore(UserData.bestScore);

                if (_needShowLeaderboards)
                {
                    _needShowLeaderboards = false;
                    showLeaderboard();
                }
            }
            else
                Debug.Log ("Authentication failed");
        });
    }

    protected virtual void _initIDs()
    {
    }


    public void sendBestScore(int points)
    {
        #if (UNITY_ANDROID || UNITY_IPHONE)  && !UNITY_EDITOR
        if (_logged)
        Social.ReportScore(points, _leaderboardId, success => {});
        #endif
    }
        
    public virtual void showLeaderboard()
    {
        if (_logged)
        {
            Social.ShowLeaderboardUI();
        }
        else
        {
            _needShowLeaderboards = true;
            _authenticate();
        }
    }

}

