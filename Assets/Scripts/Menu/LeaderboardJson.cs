using System;

[Serializable]
public class LeaderboardJson
{
    // Рейтинг игрока
    public int rating;
    // Имена лучших игроков
    public string[] names;
    // Результаты лучших игроков
    public long[] results;
}