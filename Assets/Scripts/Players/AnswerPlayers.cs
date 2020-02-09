using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPlayers : MonoBehaviour
{
    [Header("Набор плиток")]
    [SerializeField] private ButtonsBall buttons;

    [Header("Поле для ввода ответа")]
    [SerializeField] InputField input;

    [Header("Кнопки подсказок")]
    [SerializeField] private GameObject[] tipsButtons;

    [Header("Текст для команды")]
    [SerializeField] private Text team;

    [Header("Описание задания")]
    [SerializeField] private Text description;

    [Header("Эффект победы")]
    [SerializeField] private ParticleSystem particle;

    // Ссылки на компоненты
    private Text textAnswer;
    private Outline outline;
    private TasksPlayers photos;
    private InfoPlayers information;
    private Button nextLevel;
    private Statistics statistics;

    private void Awake()
    {
        textAnswer = GetComponent<Text>();
        outline = GetComponent<Outline>();
    }

    private void Start()
    {
        photos = Camera.main.GetComponent<TasksPlayers>();
        information = GameObject.FindGameObjectWithTag("Tasks").GetComponent<InfoPlayers>();
        nextLevel = information.gameObject.GetComponent<Button>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    /// <summary>
    /// Проверка ответа игрока
    /// </summary>
    /// <param name="answer">Свой ответ или пропуск</param>
    public void ComparisonAnswers(bool answer)
    {
        // Получаем ответы для текущего уровня
        var name = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Name;
        var lastname = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Lastname;

        // Ответ игрока
        string playerResponse;

        // Если дан ответ, получаем его с поля ввода
        if (answer) GetTextInput(out playerResponse);
        // Иначе устанавливаем правильный ответ
        else playerResponse = lastname;

        // Если полученный ответ совпадает с правильным ответом
        if (playerResponse == lastname || playerResponse == (name + lastname))
        {
            // Отключаем поле для ввода
            input.interactable = false;

            // Скрываем все подсказки на уровне
            for (int i = 0; i < tipsButtons.Length; i++) tipsButtons[i].SetActive(false);
            // Останавливаем все отсчеты для подсказок
            StopAllCoroutines();

            // Выводим название команды и полное имя футболиста
            team.text = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Team;
            textAnswer.text = name + ((name != "") ? " " : "") + lastname;

            // Выводим результат в описание
            PrintResultTask(answer);

            // Скрываем все плитки с изображения
            buttons.UpdateButtons(true);

            // Увеличиваем прогресс категории
            IncreaseProgress(answer);

            // Активируем переход к следующему заданию
            nextLevel.interactable = true;
        }
        // Если ответ неправильный
        else if (playerResponse != "")
        {
            // Сообщаем о неправильном ответе
            textAnswer.text = "Н е в е р н о";
            // Отображаем обводку ответа
            outline.enabled = true;

            // Увеличиваем общее количество ошибок
            PlayerPrefs.SetInt(Photos.category + "-errors", PlayerPrefs.GetInt(Photos.category + "-errors") + 1);

            // Через несколько секунд сбрасываем ответ
            Invoke("ResetAnswerText", 1.3f);
        }
        else
        {
            // Если ответ пустой, сбрасываем его
            ResetAnswerText();
        }
    }

    /// <summary>
    /// Получание ответа из поля ввода
    /// </summary>
    /// <param name="answer"></param>
    private void GetTextInput(out string answer)
    {
        answer = textAnswer.text.ToLower();
        // Удаляем пробелы из ответа
        answer = answer.Replace(" ", "");
    }

    /// <summary>
    /// Вывод результата после получения правильного ответа
    /// </summary>
    /// <param name="answer">Свой ответ или пропуск</param>
    private void PrintResultTask(bool answer)
    {
        if (answer)
        {
            // Выводим сообщение с поздравлением
            description.text = "Великолепно!" + Indents.LineBreak(1) + "Это правильный ответ.";
            // Воспроизводим победный эффект
            particle.Play();
        }
        else
        {
            // Иначе выводим сообщение о пропуске
            description.text = "Без бонуса!" + Indents.LineBreak(1) + "Использован пропуск задания.";
        }
    }

    /// <summary>
    /// Сброс текстового поля ответа
    /// </summary>
    private void ResetAnswerText()
    {
        // Отображаем стандартный текст
        textAnswer.text = "В в е с т и   о т в е т";
        // Сбрасываем красную обводку текста
        outline.enabled = false;
    }

    /// <summary>
    /// Увеличение прогресса викторины
    /// </summary>
    /// <param name="answer">Свой ответ или пропуск</param>
    private void IncreaseProgress(bool answer)
    {
        // Увеличиваем прогресс активной категории
        PlayerPrefs.SetInt(Photos.category, PlayerPrefs.GetInt(Photos.category) + 1);

        if (answer)
        {
            // Увеличиваем счет и монеты
            statistics.ChangeTotalScore(3);
            statistics.ChangeTotalCoins(75);
        }
        else
        {
            // Вычитаем штрафные монеты
            statistics.ChangeTotalCoins(-80);
        }
    }

    /// <summary>
    /// Обновление задания
    /// </summary>
    public void TaskUpdate()
    {
        // Если текущий прогресс не превышает количество заданий
        if (PlayerPrefs.GetInt(Photos.category) < information.QuantityPhotos)
        {
            // Восстанавливаем плитки на изображении
            buttons.UpdateButtons(false);

            // Активируем поле для ввода ответа
            input.interactable = true;

            // Отключаем переход к следующему заданию
            nextLevel.interactable = false;

            // Устанавливаем новое изображение
            information.ShowTaskImage();

            // Удаляем две случайные плитки
            buttons.RemoveRandomButtons(2);

            // Проверяем доступность подсказки
            photos.CheckHint();

            // Сбрасываем тексты с описанием задания и названием команды
            description.text = "Открывай дополнительные фрагменты за 35 монет";
            team.text = "* * * * *";

            // Сбрасываем поле ответа
            ResetAnswerText();
        }
        else
        {
            // Если все задания пройдены, переходим к результатам
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(7);
        }
    }

    /// <summary>
    /// Отображение кнопки пропуска задания
    /// </summary>
    public void ShowSkipButton()
    {
        if (PlayerPrefs.GetInt("coins") > 80)
            // Отображаем кнопку через несколько секунд
            StartCoroutine(ButtonPass(2.5f));
    }

    /// <summary>
    /// Отсчет до появления кнопки пропуска
    /// </summary>
    /// <param name="seconds">Количество секунд</param>
    private IEnumerator ButtonPass(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        tipsButtons[1].SetActive(true);
    }

    /// <summary>
    /// Отображение названия команды
    /// </summary>
    public void ShowTeam()
    {
        // Выводим название команды
        team.text = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Team;

        // Вычитаем стоимость подсказки
        statistics.ChangeTotalCoins(-50);
        // Увеличиваем количество использованных подсказок
        PlayerPrefs.SetInt(Photos.category + "-tips", PlayerPrefs.GetInt(Photos.category + "-tips") + 1);
    }

    /// <summary>
    /// Пропуск задания
    /// </summary>
    public void SkipTask()
    {
        // Проверяем ответ с указанием пропуска
        ComparisonAnswers(false);

        // Увеличиваем количество использованных пропусков заданий
        PlayerPrefs.SetInt(Photos.category + "-pass", PlayerPrefs.GetInt(Photos.category + "-pass") + 1);
    }
}