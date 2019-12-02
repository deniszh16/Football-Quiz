using System;
using UnityEngine;

public class TasksCountries : FileProcessing
{
    // Объект для работы с json по вопросам
    private QueJson tasks = new QueJson();
    public QueJson Tasks { get { return tasks; } }

    // Объект для работы с json по наборам уровней
    public SetJson Sets { get; private set; } = new SetJson();

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    [Header("Панель вариантов")]
    [SerializeField] private GameObject variants;

    [Header("Кнопка подсказок")]
    [SerializeField] private GameObject buttonTips;

    [Header("Кнопка удаления букв")]
    [SerializeField] private GameObject buttonDelete;

    [Header("Компонент ответа")]
    [SerializeField] private AnswerCountries answer;
    public AnswerCountries Answer { get { return answer; } }

    // Первая буква ответа (для подсказки)
    public string FirstLetter { get; private set; }

    // Прогресс в текущей категории
    public int Progress { get; set; }

    private void Awake()
    {
        // Обрабатываем json файл и записываем в текстовую переменную
        var jsonString = ReadJsonFile("category-" + Categories.category.ToString());
        // Преобразовываем строку в объект
        ConvertToObject(ref tasks, jsonString);

        // Преобразуем json строку по категориям в объект 
        Sets = JsonUtility.FromJson<SetJson>(PlayerPrefs.GetString("sets"));

        // Записываем текущий прогресс категории
        Progress = Sets.ArraySets[Categories.category];
    }

    private void Start()
    {
        SceneSetting();
    }

    /// <summary>Настройка сцены под текущий тип вопроса</summary>
    public void SceneSetting()
    {
        if (Tasks.TaskItems[Progress - 1].Type == "letters")
        {
            // Активируем панель с буквами
            letters.SetActive(true);
            // Активируем кнопку подсказок
            buttonTips.SetActive(true);
            // Активируем кнопку стирания букв
            buttonDelete.SetActive(true);

            // Получаем первую букву ответа (для подсказки)
            FirstLetter = Tasks.TaskItems[Progress - 1].Answer[0];
        }
        else
        {
            // Активируем панель с вариантами
            variants.SetActive(true);
        }

        // Настраиваем ответ для задания
        answer.TypeAnswer(Tasks.TaskItems[Progress - 1].Type);
    }

    /// <summary>Поиск первой буквы ответа в массиве букв</summary>
    public int LetterSearch()
    {
        // Возвращаем номер первого вхождения в массив
        return Array.IndexOf(Tasks.TaskItems[Progress - 1].Letters, FirstLetter);
    }

    /// <summary>Поиск указанной буквы в массиве ответа</summary>
    public int LetterSearch(int number)
    {
        return Array.IndexOf(Tasks.TaskItems[Progress - 1].Answer, Tasks.TaskItems[Progress - 1].Letters[number]);
    }
}