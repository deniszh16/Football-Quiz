using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AnswerPlayers : MonoBehaviour
{
    private Text textAnswer;
    private Outline outline;

    private TasksPlayers photos;
    private ImageList imageList;
    private Button buttonLevel;
    private Statistics statistics;

    [Header("Набор плиток")]
    [SerializeField] private ButtonsBall buttons;

    [Header("Поле для ввода")]
    [SerializeField] InputField input;

    [Header("Кнопка подсказки")]
    [SerializeField] private GameObject tips;

    [Header("Кнопка пропуска")]
    [SerializeField] private GameObject buttonPass;

    [Header("Название команды")]
    [SerializeField] private Text team;

    [Header("Описание задания")]
    [SerializeField] private Text description;

    [Header("Эффект победы")]
    [SerializeField] private ParticleSystem particle;

    private void Awake()
    {
        textAnswer = GetComponent<Text>();
        outline = GetComponent<Outline>();
        photos = Camera.main.GetComponent<TasksPlayers>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    private void Start()
    {
        imageList = GameObject.FindGameObjectWithTag("Tasks").GetComponent<ImageList>();
        buttonLevel = imageList.gameObject.GetComponent<Button>();
    }

    // Проверка ответа игрока
    public void ComparisonAnswers(bool taskSkipping = false)
    {
        // Получаем ответы для текущего уровня (с текущим прогрессом)
        string name = imageList.names[PlayerPrefs.GetInt(Modes.category)];
        string lastname = imageList.lastnames[PlayerPrefs.GetInt(Modes.category)];

        // Ответ игрока
        string answer;

        // Если не использован пропуск
        if (!taskSkipping)
        {
            // Получаем текст с клавиатуры
            answer = textAnswer.text.ToLower();
            // Удаляем из ответа все пробелы
            answer = answer.Replace(" ", "");
        }
        // Иначе устанавливаем правильный ответ
        else answer = lastname;

        // Если ответ игрока совпадает с правильным ответом
        if (answer == lastname || answer == (name + lastname))
        {
            // Отключаем поле для ввода
            input.interactable = false;

            // Скрываем подсказку
            tips.SetActive(false);
            // Останавливаем все корутины
            StopAllCoroutines();
            // Скрываем кнопку пропуска
            buttonPass.SetActive(false);
            // Выводим название команды
            team.text = imageList.teams[PlayerPrefs.GetInt(Modes.category)];
            // Выводим имя футболиста (если не пустое, добавляем пробел перед фамилией)
            textAnswer.text = name + ((name != "") ? " " : "") + lastname;

            // Если не использован пропуск
            if (!taskSkipping)
            {
                // Выводим сообщение с поздравлением
                description.text = "Великолепно!" + Indents.LineBreak(1) + "Это правильный ответ.";
                // Воспроизводим эффект победы
                particle.Play();
            }
            // Иначе выводим сообщение о пропуске
            else description.text = "Без бонуса!" + Indents.LineBreak(1) + "Использован пропуск задания.";

            // Скрываем все плитки с изображения
            photos.UpdateButtons(true);

            // Увеличиваем прогресс и обновляем статистику
            IncreaseProgress(taskSkipping);
            statistics.UpdateScore();
            statistics.UpdateCoins();

            // Активируем переход к следующему заданию
            buttonLevel.interactable = true;
        }
        // Если ответ неправильный (но не пустой)
        else if (answer != "")
        {
            // Сообщаем о неправильном ответе
            textAnswer.text = "Н е в е р н о";
            outline.enabled = true;

            // Увеличиваем количество ошибок
            PlayerPrefs.SetInt(Modes.category + "-errors", PlayerPrefs.GetInt(Modes.category + "-errors") + 1);

            // Через несколько секунд возвращаем стандартную запись
            Invoke("ResetAnswer", 1.3f);
        }
        // Если ответ пустой, сбрасываем его
        else ResetAnswer();
    }

    // Сброс ответа
    private void ResetAnswer()
    {
        // Отображаем начальный текст
        textAnswer.text = "В в е с т и   о т в е т";
        // Сбрасываем обводку текста
        outline.enabled = false;
    }

    // Увеличение прогресса викторины
    private void IncreaseProgress(bool taskSkipping)
    {
        // Увеличиваем прогресс викторины
        PlayerPrefs.SetInt(Modes.category, PlayerPrefs.GetInt(Modes.category) + 1);

        // Если не использован пропуск
        if (!taskSkipping)
        {
            // Увеличиваем общий счет и количество монет
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 3);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 75);
        }
    }

    // Обновление задания
    public void TaskUpdate()
    {
        // Если текущий прогресс не превышает количество заданий
        if (PlayerPrefs.GetInt(Modes.category) < imageList.lastnames.Length)
        {
            // Восстанавливаем все плитки
            photos.UpdateButtons(false);
            // Активируем поле для ввода
            input.interactable = true;
            // Отключаем переход к следующему заданию
            buttonLevel.interactable = false;

            // Устанавливаем новое изображение
            imageList.ShowImage();
            // Удаляем две случайные плитки
            buttons.RemoveButton();

            // Проверяем доступность подсказки
            photos.CheckHint();
            // Сбрасываем стандартную подсказку и название команды
            description.text = "Открывай дополнительные фрагменты за 35 монет";
            team.text = "* * * * *";

            // Сбрасываем текст в ответе
            ResetAnswer();
        }
        // Иначе переходим к итогам викторины
        else Camera.main.GetComponent<TransitionsInMenu>().GoToScene(7);
    }

    // Отображение кнопки пропуска
    public void ShowSkipButton()
    {
        // Если достаточно монет, отображаем кнопку пропуска через несколько секунд
        if (PlayerPrefs.GetInt("coins") > 80) StartCoroutine(ButtonPass());
    }

    private IEnumerator ButtonPass()
    {
        // Отсчет двух секунд и отображение кнопки
        yield return new WaitForSeconds(2.5f);
        buttonPass.SetActive(true);
    }

    // Пропуск вопроса
    public void SkipQuestion()
    {
        // Вычитаем монеты и обновляем статистику
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 80);
        statistics.UpdateCoins();

        // Вызываем проверку вопроса
        ComparisonAnswers(true);
    }
}