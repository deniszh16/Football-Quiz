namespace DZGames.Football.Services
{
    public interface IFirebaseService
    {
        public void Initialization();
        public void SubmitAnEvent(string id);
        public void SubmitAnEvent(string id, (string, int) parameter);
    }
}