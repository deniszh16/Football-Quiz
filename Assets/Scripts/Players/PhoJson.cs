using System;

[Serializable]
public class PhoJson
{
    // Массив игроков
    public PlayerInfo[] Players;
}

[Serializable]
public class PlayerInfo
{
    // Имя футболиста
    public string Name;
    // Фамилия футболиста
    public string Lastname;
    // Команда игрока
    public string Team;
}