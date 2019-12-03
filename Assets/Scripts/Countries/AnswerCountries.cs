using System;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCountries : MonoBehaviour
{
    [Header("Подробный ответ")]
    [SerializeField] private Text detailedAnswer;

    [Header("Скрыть при победе")]
    [SerializeField] private GameObject[] hiddenObjects;

    [Header("Победный эффект")]
    [SerializeField] private ParticleSystem particle;

    [Header("Кнопка обновления вопроса")]
    [SerializeField] private Button updateQuestion;

    // Массив букв, открытых игроком
    public string[] PlayerResponse { get; private set; }

    // Номера открытых букв
    public int[] NumbersLetters { get; set; }

    // Количество открытых букв
    public int QuantityLetters { get; set; }

    // Ссылки на используемые компоненты
    private Text textAnswer;
    private Outline outlineAnswer;
    private TasksCountries questions;
    private Statistics statistics;

    private void Awake()
    {
        textAnswer = GetComponent<Text>();
        outlineAnswer = GetComponent<Outline>();
        questions = Camera.main.GetComponent<TasksCountries>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    /// <summary>Настройка ответа для задания (тип задания)</summary>
    public void TypeAnswer(string type)
    {
        if (type == "letters")
        {
            // Создаем новый массив равный количеству букв в ответе
            PlayerResponse = new string[questions.Tasks.TaskItems[questions.Progress - 1].Answer.Length];
            // Создаем аналогичный массив под номера открытых букв
            NumbersLetters = new int[PlayerResponse.Length];

            // Обновляем поле ответа
            UpdateResponseField();
        }
        else
        {
            // Выводим три звездочки
            textAnswer.text = "*   *   *";
        }
    }

    /// <summary>Заполнение ответа выбранными буквами либо звездочками</summary>
    public void UpdateResponseField()
    {
        // Сбрасываем текст ответа
        textAnswer.text = "";

        for (int i = 0; i < PlayerResponse.Length; i++)
        {
            // Если ячейка заполнена, выводим значение (иначе звездочку)
            textAnswer.text += PlayerResponse[i] ?? "*";

            // Если символ не последний, добавляем отступ между буквами
            if (i < PlayerResponse.Length - 1) textAnswer.text += "   ";
        }
    }

    /// <summary>Сброс выбранных букв</summary>
    public void ResetLetters()
    {
        // Очищаем массивы с выбранными буквами
        Array.Clear(PlayerResponse, 0, PlayerResponse.Length);
        Array.Clear(NumbersLetters, 0, NumbersLetters.Length);

        // Обнуляем количество открытых букв
        QuantityLetters = 0;

        // Обновляем поле ответа
        UpdateResponseField();
        // Скрываем обводку ответа
        ChangeOutlineText(false);
    }

    /// <summary>Настройка обводки ответа</summary>
    public void ChangeOutlineText(bool state)
    {
        outlineAnswer.enabled = state;
    }

    /// <summary>Проверка буквенного ответа (свой ответ или пропуск)</summary>
    public void ComparisonAnswers(bool answer)
    {
        // Если массив ответа полностью совпадает с массивом выбранных букв
        if (questions.Tasks.TaskItems[questions.Progress - 1].Answer.SequenceEqual(PlayerResponse))
        {
            // Скрываем лишнее, увеличиваем прогресс
            ReceivedCorrectAnswer(answer);
        }
        // Если ответ неправильный и все буквы заполнены
        else if (PlayerResponse.Last() != null)
        {
            // Отображаем обводку
            ChangeOutlineText(true);
            // Увеличиваем количество ошибок в викторине
            PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
        }
    }

    /// <summary>Проверка ответа с вариантами (номер ответа)</summary>
    public void ComparisonAnswers(int number)
    {
        // Если выбранный вариант ответа совпадает с правильным
        if (questions.Tasks.TaskItems[questions.Progress - 1].Correct == number)
        {
            // Скрываем лишнее, увеличиваем прогресс
            ReceivedCorrectAnswer(true);
        }
        else
        {
            // Вычитаем штрафные монеты
            statistics.ChangeTotalCoins(-50);
            // Увеличиваем количество ошибок в викторине
            PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
        }
    }

    /// <summary>Действия при получении правильного ответа (свой ответ или пропуск)</summary>
    private void ReceivedCorrectAnswer(bool answer)
    {
        // Скрываем лишние объекты на сцене
        for (int i = 0; i < hiddenObjects.Length; i++)
            hiddenObjects[i].SetActive(false);

        // Проигрываем победный эффект
        if (answer) particle.Play();

        // Выводим ответ и полное описание ответа
        textAnswer.text = questions.Tasks.TaskItems[questions.Progress - 1].FullAnswer;
        detailedAnswer.text = questions.Tasks.TaskItems[questions.Progress - 1].Description;

        // Увеличиваем прогресс викторины
        IncreaseProgress(answer);
    }

    /// <summary>Увеличение прогресса викторины (свой ответ или пропуск)</summary>
    private void IncreaseProgress(bool answer)
    {
        if (answer)
        {
            // Увеличиваем счет и монеты
            statistics.ChangeTotalScore(5);
            statistics.ChangeTotalCoins(80);

            // Увеличиваем количество правильных ответов
            PlayerPrefs.SetInt("countries-answer", PlayerPrefs.GetInt("countries-answer") + 1);
        }

        // Увеличиваем прогресс категории
        questions.Sets.arraySets[Categories.category]++;
        // Сохраняем обновленное значение прогресса
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(questions.Sets));

        // Активируем кнопку для перехода к следующему вопросу
        updateQuestion.interactable = true;
    }

    /// <summary>Пропуск задания</summary>
    public void GetRightAnswer()
    {
        // Получаем правильный ответ на задание
        PlayerResponse = questions.Tasks.TaskItems[questions.Progress - 1].Answer;
        ComparisonAnswers(false);
    }

    /// <summary>Переход к следующему заданию</summary>
    public void TaskUpdate()
    {
        // Если сохраненный прогресс не превышает количество вопросов
        if (questions.Progress < questions.Tasks.TaskItems.Length)
        {
            // Перезагружаем сцену для обновления задания
            Camera.main.GetComponent<TransitionsInMenu>().RestartScene();
        }
        else
        {
            // Начисляем бонус за пройденную категорию
            statistics.ChangeTotalCoins(500);

            // Переходим в список пройденных вопросов
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(4);
        }
    }
}