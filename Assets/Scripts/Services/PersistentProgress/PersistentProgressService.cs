using DZGames.Football.Data;

namespace DZGames.Football.Services
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; private set; }

        public void SetUserProgress(UserProgress progress) =>
            GetUserProgress = progress;
    }
}