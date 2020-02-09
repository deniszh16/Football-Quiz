public static class Indents
{
    /// <summary>
    /// Создание переноса строки 
    /// </summary>
    /// <param name="repeat">Количество повторов</param>
    public static string LineBreak(int repeat)
    {
        return new string('\n', repeat);
    }

    /// <summary>
    /// Создание отделяющих черточек
    /// </summary>
    /// <param name="repeat">Количество повторов</param>
    public static string Underscore(int repeat)
    {
        return new string('-', repeat);
    }
}