public static class Indents
{
    // Создание указанного количества переносов строки
    public static string LineBreak(int repeat) { return new string('\n', repeat); }

    // Создание указанного количества черточек (для разделения текста)
    public static string Underscore(int repeat) { return new string('-', repeat); }
}