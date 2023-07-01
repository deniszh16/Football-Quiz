using System;

namespace Data
{
    [Serializable]
    public class LeaderboardData
    {
        public int MyRating;
        
        public string[] PlayersNames;
        public long[] Results;

        public LeaderboardData()
        {
            PlayersNames = new string[10];
            Results = new long[10];
        }
    }
}