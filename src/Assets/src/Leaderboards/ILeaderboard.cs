
namespace Assets.src.Leaderboards
{
    public interface ILeaderboard
    {
        void Show();
        void PublishScore(int score);
    }
}
