using System;

[Serializable]
public class QueJson
{
    // Массив заданий
    public TaskItems[] TaskItems;
}

[Serializable]
public class TaskItems
{
    // Тип вопроса
    public string Type;
    // Вопрос уровня
    public string Question;
    // Массив букв для ответа
    public string[] Letters;
    // Ответ на вопрос
    public string[] Answer;
    // Массив вариантов ответа
    public string[] Options;
    // Номер правильного варианта
    public int Correct;
    // Полный правильный ответ
    public string FullAnswer;
    // Описание правильного ответа
    public string Description;
}