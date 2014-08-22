using System.Collections.Generic;
using UnityEngine;

namespace Assets.src.Leaderboards
{
    public class AndroidLeaderboard : ILeaderboard
    {
        private readonly string _leaderboardId;

        public AndroidLeaderboard(string leaderboardId)
        {
#if UNITY_ANDROID
            Debug.Log("Init android leaderboard.");
            _leaderboardId = leaderboardId;

            PlayGameServices.enableDebugLog(true);

            GPGManager.authenticationFailedEvent += GPGManager_authenticationFailedEvent;
            GPGManager.authenticationSucceededEvent += GPGManager_authenticationSucceededEvent;
            GPGManager.submitScoreFailedEvent += GPGManager_submitScoreFailedEvent;
            GPGManager.submitScoreSucceededEvent += GPGManager_submitScoreSucceededEvent;

            Debug.Log("Try silent authentication.");
            PlayGameServices.attemptSilentAuthentication();
#else
            Debug.Log("Error. AndroidLeaderboard call for not android platform.");
#endif
        }

        #region Events
        private void GPGManager_authenticationSucceededEvent(string obj)
        {
            Debug.Log("authenticationSucceededEvent:" + obj);
        }

        private void GPGManager_authenticationFailedEvent(string obj)
        {
            Debug.Log("authenticationFailedEvent:" + obj);
        }

        private void GPGManager_submitScoreSucceededEvent(string arg1, Dictionary<string, object> arg2)
        {
            Debug.Log("submitScoreSucceededEvent, arg1:" + arg1 + ", arg2:" + arg2);
        }

        private void GPGManager_submitScoreFailedEvent(string arg1, string arg2)
        {
            Debug.Log("submitScoreFailedEvent, arg1:" + arg1 + ", arg2:" + arg2);
        }

        #endregion

        public void Show()
        {
#if UNITY_ANDROID
            Debug.Log("Try to show leaderboard.");
            if (PlayGameServices.isSignedIn())
            {
                Debug.Log("SignedIn. ShowLeaderboard.");
                PlayGameServices.showLeaderboard(_leaderboardId);
            }
            else
            {
                Debug.Log("Not SidngedIn. Authenticate.");
                PlayGameServices.authenticate();
            }
#else
            Debug.Log("Error. AndroidLeaderboard.Show call for not android platform.");
#endif
        }

        public void PublishScore(int score)
        {
#if UNITY_ANDROID
            if (PlayGameServices.isSignedIn())
            {
                Debug.Log("Publish score. Signed In.");
                PlayGameServices.submitScore(_leaderboardId, score);
            }
            else
            {
                Debug.Log("Publish score not possible. Not Signed In.");
            }
#else
            Debug.Log("Error. AndroidLeaderboard.Show call for not android platform.");
#endif
        }
    }

}
