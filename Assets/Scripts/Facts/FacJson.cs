using System;

[Serializable]
public class FacJson
{
    // Массив заданий
    public FactItems[] Facts;
}

[Serializable]
public class FactItems
{
    // Вопрос задания
    public string Question;
    // Проверка ответа
    public bool Answer;
    // Описание ответа
    public string Description;
}