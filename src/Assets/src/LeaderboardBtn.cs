using Assets.src.Leaderboards;
using UnityEngine;

public class LeaderboardBtn : MonoBehaviour
{
    public tk2dUIItem btn;

    public void Start()
    {
        btn.OnClick += click;
    }

    void click()
    {
        LeaderboardManager.OpenLeaderboard();
    }
}
