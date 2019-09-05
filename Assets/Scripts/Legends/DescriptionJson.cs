using System;

[Serializable]
public class DescriptionJson
{
    // Имя футболиста
    public string name;
    // Достижения футболиста
    public ProgressLegend progress;
}

[Serializable]
public class ProgressLegend
{
    // Клубные достижения
    public string club;
    // Достижения в сборной
    public string team;
    // Особые достижения
    public string personal;
}