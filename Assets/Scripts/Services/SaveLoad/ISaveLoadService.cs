using DZGames.Football.Data;

namespace DZGames.Football.Services
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}