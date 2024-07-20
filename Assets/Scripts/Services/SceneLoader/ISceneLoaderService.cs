namespace DZGames.Football.Services
{
    public interface ISceneLoaderService
    {
        public void Load(string sceneName);
        public void Load(string sceneName, float delay);
    }
}