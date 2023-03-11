using Code.Data;
using Code.Services.PersistentProgress;

namespace Code.Services.SaveLoad
{
    public interface ISaveLoadService
    {
        public void Construct(IPersistentProgressService persistentProgressService);
        public void SaveProgress();
        public UserProgress LoadProgress();
    }
}