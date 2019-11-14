public static class Indents
{
    /// <summary>Создание переноса строки (количество повторов)</summary>
    public static string LineBreak(int repeat)
    {
        return new string('\n', repeat);
    }

    /// <summary>Создание отделяющих черточек (количество повторов)</summary>
    public static string Underscore(int repeat)
    {
        return new string('-', repeat);
    }
}