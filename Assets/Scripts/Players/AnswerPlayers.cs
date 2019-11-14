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

    // Ссылки на компоненты ответа
    private Text textAnswer;
    private Outline outline;

    // Ссылки на сторонние компоненты
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

    /// <summary>Проверка ответа игрока (свой ответ или пропуск)</summary>
    public void ComparisonAnswers(bool answer = false)
    {
        // Получаем ответы для текущего уровня (с текущим прогрессом)
        var name = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Name;
        var lastname = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Lastname;

        // Ответ игрока
        string playerResponse;

        // Если не использован пропуск
        if (!answer)
        {
            // Записываем в ответ текст с клавиатуры
            playerResponse = textAnswer.text.ToLower();
            // Удаляем из ответа все пробелы
            playerResponse = playerResponse.Replace(" ", "");
        }
        else
        {
            // Устанавливаем правильный ответ
            playerResponse = lastname;
        }

        // Если пользовательский ответ совпадает с правильным ответом
        if (playerResponse == lastname || playerResponse == (name + lastname))
        {
            // Отключаем поле для ввода
            input.interactable = false;

            // Скрываем все подсказки
            for (int i = 0; i < tipsButtons.Length; i++) tipsButtons[i].SetActive(false);
            // Останавливаем все отсчеты
            StopAllCoroutines();

            // Выводим название команды
            team.text = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Team;
            // Выводим имя футболиста (если не пустое, добавляем пробел перед фамилией)
            textAnswer.text = name + ((name != "") ? " " : "") + lastname;

            // Если не использован пропуск
            if (!answer)
            {
                // Выводим сообщение с поздравлением
                description.text = "Великолепно!" + Indents.LineBreak(1) + "Это правильный ответ.";

                // Воспроизводим эффект победы
                particle.Play();
            }
            else
            {
                // Иначе выводим сообщение о пропуске
                description.text = "Без бонуса!" + Indents.LineBreak(1) + "Использован пропуск задания.";
            }

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
            // Отображаем обводку
            outline.enabled = true;

            // Увеличиваем общее количество ошибок
            PlayerPrefs.SetInt(Photos.category + "-errors", PlayerPrefs.GetInt(Photos.category + "-errors") + 1);

            // Через несколько секунд сбрасываем ответ
            Invoke("ResetAnswer", 1.3f);
        }
        else
        {
            // Если ответ пустой, сбрасываем текстовое поле
            ResetAnswer();
        }
    }

    /// <summary>Сброс текстового поля ответа</summary>
    private void ResetAnswer()
    {
        // Отображаем стандартный текст
        textAnswer.text = "В в е с т и   о т в е т";
        // Сбрасываем красную обводку текста
        outline.enabled = false;
    }

    /// <summary>Увеличение прогресса викторины (свой ответ или пропуск)</summary>
    private void IncreaseProgress(bool answer)
    {
        // Увеличиваем прогресс активной категории
        PlayerPrefs.SetInt(Photos.category, PlayerPrefs.GetInt(Photos.category) + 1);

        // Если не использован пропуск
        if (!answer)
        {
            // Увеличиваем общий счет и количество монет
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 3);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 75);

            // Обновляем статистику
            statistics.UpdateScore();
        }
        else
        {
            // Вычитаем штрафные монеты
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 80);
        }

        // Обновляем количество монет
        statistics.UpdateCoins();
    }

    /// <summary>Обновление задания</summary>
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

            // Проверяем подсказку
            photos.CheckHint();

            // Сбрасываем тексты с описанием задания и названием команды
            description.text = "Открывай дополнительные фрагменты за 35 монет";
            team.text = "* * * * *";

            // Сбрасываем текстовое поле ответа
            ResetAnswer();
        }
        else
        {
            // Если все задания пройдены, переходим к результатам
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(7);
        }
    }

    /// <summary>Отображение кнопки пропуска задания</summary>
    public void ShowSkipButton()
    {
        // Если достаточно монет
        if (PlayerPrefs.GetInt("coins") > 80)
            // Отображаем кнопку через несколько секунд
            StartCoroutine(ButtonPass(2.5f));
    }

    /// <summary>Отсчет до появления кнопки (количество секунд)</summary>
    private IEnumerator ButtonPass(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        // Отображаем кнопку пропуска
        tipsButtons[1].SetActive(true);
    }

    /// <summary>Отображение названия команды</summary>
    public void ShowTeam()
    {
        // Выводим название команды
        team.text = information.PhoJson.Players[PlayerPrefs.GetInt(Photos.category)].Team;

        // Вычитаем стоимость подсказки
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 50);
        // Обновляем статистику с анимацией мигания
        statistics.UpdateCoins(true);

        // Увеличиваем количество использованных подсказок
        PlayerPrefs.SetInt(Photos.category + "-tips", PlayerPrefs.GetInt(Photos.category + "-tips") + 1);
    }

    /// <summary>Пропуск задания</summary>
    public void SkipTask()
    {
        // Выполняем проверку ответа с указанием пропуска
        ComparisonAnswers(true);

        // Увеличиваем количество использованных пропусков заданий
        PlayerPrefs.SetInt(Photos.category + "-pass", PlayerPrefs.GetInt(Photos.category + "-pass") + 1);
    }
}