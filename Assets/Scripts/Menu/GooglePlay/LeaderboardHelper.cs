using System;

namespace Cubra.Helpers
{
    [Serializable]
    public class LeaderboardHelper
    {
        // Рейтинг игрока
        public int Rating;

        // Результаты лучших
        public string[] Names;
        public long[] Results;
    }
}