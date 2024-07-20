using System;

namespace DZGames.Football.Data
{
    [Serializable]
    public class PlayersData
    {
        public int[] Sets = new int[2];
        public int Completed;
        public int RightAnswers;
        public int WrongAnswers;
    }
}