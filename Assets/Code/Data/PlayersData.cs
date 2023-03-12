using System;

namespace Code.Data
{
    [Serializable]
    public class PlayersData
    {
        public int[] Sets;
        public int Completed;
        public int RightAnswers;
        public int WrongAnswers;

        public PlayersData() =>
            Sets = new int[2];
    }
}