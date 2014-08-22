using UnityEngine;

namespace Assets.src.Leaderboards
{
    public class LeaderboardManager : MonoBehaviour
    {
        private ILeaderboard _leaderboard;
        private static LeaderboardManager _instance;

        public string AndroidLeaderboardId;
        public string IOSLeaderboardId;

        public void Awake()
        {
            _instance = this;

            if (Application.platform == RuntimePlatform.Android)
                _leaderboard = new AndroidLeaderboard(AndroidLeaderboardId);
            else if (Application.platform == RuntimePlatform.IPhonePlayer)
                _leaderboard = new IOSLeaderboard(IOSLeaderboardId);
            else
                Debug.Log("Can't create leaderboard for not andoird and ios platform");
        }

        public static void OpenLeaderboard()
        {
            if (_instance._leaderboard != null)
                _instance._leaderboard.Show();
        }

        public static void PublishScore(int score)
        {
            if (_instance._leaderboard != null)
            {
                Debug.Log("Publish score to leaderboard:" + score);
                _instance._leaderboard.PublishScore(score);
            }
        }
    }
}
