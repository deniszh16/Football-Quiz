namespace Services.GooglePlay
{
    public interface IGooglePlayService
    {
        public bool Authenticated { get; }
        public void ActivateService();
        public void SignGooglePlay();
        public void ShowAchievements();
        public void UnlockAchievement(string id);
        public void ShowLeaderboard();
        public void SubmitScoreToLeaderboard(int score);
    }
}