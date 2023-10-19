using GooglePlayGames;
using UnityEngine;

namespace Services.GooglePlay
{
    public class GooglePlayService : IGooglePlayService
    {
        public bool Authenticated =>
            Social.localUser.authenticated;

        public void ActivateService() =>
            PlayGamesPlatform.Activate();

        public void SignGooglePlay()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
                Social.localUser.Authenticate(_ => {});
        }

        public void ShowAchievements()
        {
            if (Authenticated) Social.ShowAchievementsUI();
            else SignGooglePlay();
        }

        public void UnlockAchievement(string id)
        {
            if (Authenticated)
                Social.ReportProgress(id, 100.0f, _ => {});
        }

        public void ShowLeaderboard()
        {
            if (Authenticated) PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard);
            else SignGooglePlay();
        }

        public void SubmitScoreToLeaderboard(int score)
        {
            if (Authenticated)
                Social.ReportScore(score, GPGSIds.leaderboard, _ => {});
        }
    }
}