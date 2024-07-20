using System;

namespace DZGames.Football.Data
{
    [Serializable]
    public class LeaderboardData
    {
        public int MyRating;
        
        public string[] PlayersNames = new string[10];
        public long[] Results = new long[10];
    }
}