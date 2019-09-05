using UnityEngine;

public class TasksCountries : JsonFileProcessing
{
    // Объект для работы с вопросами в json файле
    public QuestionsJson Tasks { get; private set; } = new QuestionsJson();

    // Объект для работы с json по наборам уровней
    public SetsJson Sets { get; private set; } = new SetsJson();

    [Header("Режимы игры")]
    [SerializeField] private GameObject[] gameModes;

    [Header("Кнопка подсказок")]
    [SerializeField] private GameObject buttonTips;

    [Header("Кнопка стирания ответа")]
    [SerializeField] private GameObject buttonDelete;

    [Header("Компонент ответа")]
    [SerializeField] private AnswerCountries answer;
    public AnswerCountries Answer { get { return answer; } }

    // Первая буква ответа (для подсказки)
    public string FirstLetter { get; private set; }

    // Прогресс в категории
    public int Progress { get; set; }

    protected override void Awake()
    {
        // Обрабатываем json файл и записываем в переменную
        string jsonString = ReadJsonFile("category-" + Categories.category.ToString());
        // Преобразовываем строку в объект
        ConvertToObject(jsonString);

        // Преобразуем json строку по категориям в объект 
        Sets = JsonUtility.FromJson<SetsJson>(PlayerPrefs.GetString("sets"));

        // Получаем компонент ответов
        answer = answer.GetComponent<AnswerCountries>();

        // Записываем текущий прогресс категории
        Progress = Sets.arraySets[Categories.category];
    }

    // Преобразование json строки в объект
    private void ConvertToObject(string json) { Tasks = JsonUtility.FromJson<QuestionsJson>(json); }

    protected override void Start() { SceneSetting(); }

    // Настройка сцены под текущий вопрос
    public void SceneSetting()
    {
        // Если задание с буквами
        if (Tasks.questions[Progress - 1].type == "letters")
        {
            // Активируем панель с буквами
            gameModes[0].SetActive(true);
            // Активируем кнопку подсказок
            buttonTips.SetActive(true);
            // Активируем кнопку стирания букв
            buttonDelete.SetActive(true);

            // Получаем первую букву ответа (для подсказки)
            FirstLetter = Tasks.questions[Progress - 1].answer[0];

            // Настраиваем массив под ответ
            answer.TypeAnswer("letters");
        }
        else
        {
            // Активируем панель с вариантами
            gameModes[1].SetActive(true);

            // Настраиваем ответ под задание
            answer.TypeAnswer("quiz");
        }
    }
}