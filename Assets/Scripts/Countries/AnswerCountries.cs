using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCountries : MonoBehaviour
{
    [Header("Текст для подробного ответа")]
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

    // Ссылки на компоненты
    private Text textAnswer;
    public Outline Outline { get; private set; }
    private TasksCountries questions;
    private Statistics statistics;

    private void Awake()
    {
        textAnswer = GetComponent<Text>();
        Outline = GetComponent<Outline>();
        questions = Camera.main.GetComponent<TasksCountries>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    /// <summary>Подготовка ответа для задания (тип задания)</summary>
    public void TypeAnswer(string type)
    {
        // Если задание с буквами
        if (type == "letters")
        {
            // Создаем новый массив равный количеству букв в ответе
            PlayerResponse = new string[questions.Tasks.TaskItems[questions.Progress - 1].Answer.Length];
            // Создаем аналогичный массив под номера открытых букв
            NumbersLetters = new int[PlayerResponse.Length];

            // Обновляем поле ответа
            UpdateAnswer();
        }
        else
        {
            // Иначе в поле ответа выводим три звезды
            textAnswer.text = "*   *   *";
        }
    }

    /// <summary>Обновление текстового поля ответа</summary>
    public void UpdateAnswer()
    {
        // Сбрасываем текст ответа
        textAnswer.text = "";

        for (int i = 0; i < PlayerResponse.Length; i++)
        {
            // Если ячейка в массиве заполнена, выводим значение (иначе звездочку)
            textAnswer.text += (PlayerResponse[i] != null) ? PlayerResponse[i] : "*";

            // Если символ не последний, добавляем отступ между буквами
            if (i < PlayerResponse.Length - 1) textAnswer.text += "   ";
        }
    }

    /// <summary>Сброс выбранных букв (обновление текстового поля)</summary>
    public void ResetLetters(bool updateField)
    {
        // Заполняем массивы ответа пустыми значениями
        for (int i = 0; i < PlayerResponse.Length; i++)
        {
            PlayerResponse[i] = null;
            NumbersLetters[i] = 0;
        }

        // Сбрасываем количество открытых букв
        QuantityLetters = 0;

        // Обновляем текстовое поле ответа
        if (updateField) UpdateAnswer();
        // Если отображается обводка текста, скрываем ее
        if (Outline) Outline.enabled = false;
    }

    /// <summary>Проверка буквенного ответа (свой ответ или пропуск)</summary>
    public void ComparisonAnswers(bool answer)
    {
        // Если массив ответа полностью совпадает с массивом выбранных букв
        if (questions.Tasks.TaskItems[questions.Progress - 1].Answer.SequenceEqual(PlayerResponse))
        {
            // Обрабатываем правильный ответ
            CorrectAnswer(answer);

            // Выводим полное описание ответа
            Invoke("ShowDescription", 0.2f);
        }
        // Если ответ неправильный и все буквы заполнены
        else if (PlayerResponse.Last() != null)
        {
            // Отображаем красную обводку
            Outline.enabled = true;

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
            // Обрабатываем правильный ответ
            CorrectAnswer();

            // Выводим полное описание ответа
            Invoke("ShowDescription", 0.2f);
        }
        else
        {
            // Иначе вычитаем пятьдесят штрафных монет
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 50);
            // Обновляем статистику
            statistics.UpdateCoins(true);

            // Увеличиваем количество ошибок в викторине
            PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
        }
    }

    /// <summary>Обработка правильного ответа (свой ответ или пропуск)</summary>
    private void CorrectAnswer(bool answer = true)
    {
        // Скрываем все лишние объекты на сцене
        for (int i = 0; i < hiddenObjects.Length; i++)
        {
            hiddenObjects[i].SetActive(false);
        }

        // Выводим полный ответ в текстовое поле
        textAnswer.text = questions.Tasks.TaskItems[questions.Progress - 1].FullAnswer;

        // Проигрываем победный эффект
        if (answer) particle.Play();

        // Увеличиваем прогресс викторины
        IncreaseProgress(answer);
    }

    /// <summary>Вывод полного описания правильного ответа</summary>
    private void ShowDescription()
    {
        detailedAnswer.text = questions.Tasks.TaskItems[questions.Progress - 1].Description;
    }

    /// <summary>Увеличение прогресса викторины (свой ответ или пропуск)</summary>
    private void IncreaseProgress(bool answer)
    {
        if (answer)
        {
            // Увеличиваем общий счет, количество монет и правильных ответов
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + 5);
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 80);
            PlayerPrefs.SetInt("countries-answer", PlayerPrefs.GetInt("countries-answer") + 1);

            // Обновляем статистику
            statistics.UpdateCoins();
            statistics.UpdateScore();
        }

        // Увеличиваем прогресс категории
        questions.Sets.ArraySets[Categories.category]++;
        // Сохраняем обновленное значение
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(questions.Sets));

        // Активируем кнопку для перехода к следующему вопросу
        updateQuestion.interactable = true;
    }

    /// <summary>Пропуск задания</summary>
    public void GetRightAnswer()
    {
        // Устанавливаем в пользовательский ответ массив с правильным ответом
        PlayerResponse = questions.Tasks.TaskItems[questions.Progress - 1].Answer;

        // Вызываем проверку ответа (с указанием пропуска)
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
            // Иначе начисляем бонус за пройденную категорию
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 500);

            // Переходим в список пройденных вопросов
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(4);
        }
    }
}