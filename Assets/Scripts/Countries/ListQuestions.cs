using UnityEngine;
using UnityEngine.UI;

public class ListQuestions : FileProcessing
{
    [Header("Текст с вопросами")]
    [SerializeField] private Text questions;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;

    // Объект для работы с вопросами в json файле
    private QueJson tasks = new QueJson();

    private void Awake()
    {
        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile("category-" + Categories.category.ToString());
        // Преобразовываем строку в объект
        ConvertToObject(ref tasks, jsonString);
    }

    private void Start()
    {
        for (int i = 0; i < tasks.TaskItems.Length; i++)
        {
            // Выводим вопрос
            questions.text += Indents.LineBreak(1) + tasks.TaskItems[i].Question + Indents.LineBreak(2);
            // Выводим ответ на вопрос
            questions.text += "Ответ: " + tasks.TaskItems[i].FullAnswer + Indents.LineBreak(1);

            // Создаем отделяющую черту
            questions.text += Indents.Underscore(26) + Indents.LineBreak(1);
        }

        // Перемещаем скролл вверх
        scroll.verticalNormalizedPosition = 1;
    }
}