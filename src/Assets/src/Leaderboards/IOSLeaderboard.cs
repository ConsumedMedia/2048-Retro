using System;
using UnityEngine;

namespace Assets.src.Leaderboards
{
    public class IOSLeaderboard : ILeaderboard
    {
        private readonly string _leaderboardId;

        public IOSLeaderboard(string leaderboardId)
        {
            _leaderboardId = leaderboardId;
            Authenticate(null);            
        }

        private void Authenticate(Action successCallback)
        {
			Debug.Log ("Leaderboard authentification trying.");
            Social.localUser.Authenticate(success =>
            {
                Debug.Log("Leaderboard authentification success:" + success);
                if (success && successCallback != null)
                    successCallback();
            });
        }

        public void Show()
        {
            if (Social.localUser.authenticated)
                ShowLeaderboardUI();
            else
                Authenticate(ShowLeaderboardUI);
        }

        private void ShowLeaderboardUI()
        {
            Debug.Log("Open leaderboard.");
            Social.ShowLeaderboardUI();
        }

        public void PublishScore(int score)
        {
            if (Social.localUser.authenticated)
            {
                Debug.Log("Try to publish score:" + score);
                Social.ReportScore(score, _leaderboardId,
                    success => Debug.Log("ReportScore success:" + success));
            }
		}

    }
}
