using UnityEngine;
using UnityEngine.UI;

public class ListQuestions : JsonFileProcessing
{
    [Header("Текст с вопросами")]
    [SerializeField] private Text questions;

    [Header("Компонент скролла")]
    [SerializeField] private ScrollRect scroll;

    // Объект для работы с вопросами в Json файле
    private QuestionsJson tasks = new QuestionsJson();

    protected override void Awake()
    {
        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile("category-" + Categories.category.ToString());
        // Преобразовываем строку в объект
        ConvertToObject(jsonString);

        questions = questions.GetComponent<Text>();
        scroll = scroll.GetComponent<ScrollRect>();
    }

    // Преобразование json строки в объект
    private void ConvertToObject(string json) { tasks = JsonUtility.FromJson<QuestionsJson>(json); }

    protected override void Start()
    {
        // Вывод всех вопросов с ответами в текстовом поле
        for (int i = 0; i < tasks.questions.Length; i++)
        {
            questions.text += Indents.LineBreak(1) + tasks.questions[i].question + Indents.LineBreak(1) + "Ответ: " + tasks.questions[i].full_answer + Indents.LineBreak(2);
        }

        // Перемещаем скролл в самый верх
        scroll.verticalNormalizedPosition = 1;
    }
}