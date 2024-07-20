using DZGames.Football.Data;

namespace DZGames.Football.Services
{
    public interface IPersistentProgressService
    {
        public UserProgress GetUserProgress { get; }
        public void SetUserProgress(UserProgress progress);
    }
}