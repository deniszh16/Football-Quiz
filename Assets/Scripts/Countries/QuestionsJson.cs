using System;

[Serializable]
public class QuestionsJson
{
    // Массив заданий
    public TaskLevel[] questions;
}

[Serializable]
public class TaskLevel
{
    // Тип вопроса
    public string type;
    // Вопрос уровня
    public string question;
    // Массив букв для ответа
    public string[] letters;
    // Ответ на вопрос
    public string[] answer;
    // Массив вариантов ответа
    public string[] options;
    // Номер правильного варианта
    public int correct;
    // Полный правильный ответ
    public string full_answer;
    // Описание правильного ответа
    public string description;
}