using Code.Data;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}