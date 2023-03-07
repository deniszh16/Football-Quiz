using Code.Data;

namespace Code.Services.PersistentProgress
{
    public interface IPersistentProgressService
    {
        public UserProgress UserProgress { get; set; }
    }
}