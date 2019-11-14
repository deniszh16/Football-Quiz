using System;

[Serializable]
public class LeaJson
{
    // Рейтинг игрока
    public int Rating;

    // Имена лучших игроков
    public string[] Names;

    // Результаты лучших игроков
    public long[] Results;
}