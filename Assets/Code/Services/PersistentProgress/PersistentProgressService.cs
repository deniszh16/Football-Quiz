using Code.Data;

namespace Code.Services.PersistentProgress
{
    public class PersistentProgressService : IPersistentProgressService
    {
        public UserProgress UserProgress { get; set; }
    }
}