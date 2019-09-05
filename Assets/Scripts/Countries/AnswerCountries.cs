using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnswerCountries : MonoBehaviour
{
    private Text textAnswer;
    private Outline outline;
    private TasksCountries questions;
    private Statistics statistics;

    [Header("Описание ответа")]
    [SerializeField] private Text responseDescription;

    [Header("Скрыть при победе")]
    [SerializeField] private GameObject[] hiddenObjects;

    [Header("Эффект победы")]
    [SerializeField] private ParticleSystem particle;

    [Header("Следующий вопрос")]
    [SerializeField] private Button nextQuestion;

    // Массив из букв, введенных игроком
    public string[] PlayerResponse { get; set; }

    // Количество открытых букв игроком
    public int OpenLetters { get; set; }

    // Номера открытых букв игроком
    public int[] NumberOpen { get; set; }

    private void Awake()
    {
        textAnswer = GetComponent<Text>();
        outline = GetComponent<Outline>();
        questions = Camera.main.GetComponent<TasksCountries>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    // Подготовка ответа для задания
    public void TypeAnswer(string type)
    {
        // Если задание с буквами
        if (type == "letters")
        {
            // Создаем новый массив равный количеству букв в ответе
            PlayerResponse = new string[questions.Tasks.questions[questions.Progress - 1].answer.Length];
            // Создаем аналогичный массив под номера открытых букв
            NumberOpen = new int[PlayerResponse.Length];
            // Обновляем текстовое поле ответа
            UpdateAnswer();
        }
        // Иначе в поле ответа выводим три звезды
        else textAnswer.text = "*   *   *";
    }

    // Обновление текстового поля ответа
    public void UpdateAnswer()
    {
        // Сбрасываем текст ответа
        textAnswer.text = "";

        for (int i = 0; i < PlayerResponse.Length; i++)
        {
            // Если ячейка в массиве заполнена, выводим значение (иначе звездочку)
            textAnswer.text += (PlayerResponse[i] != null) ? PlayerResponse[i] : "*";
            // Для каждого символа, кроме последнего, добавляем отступ
            if (i < PlayerResponse.Length - 1) textAnswer.text += "   ";
        }
    }

    // Сброс выбранных букв
    public void ResetLetters(bool updateFieldAnswer)
    {
        // Заполняем массивы ответа пустыми значениями
        for (int i = 0; i < PlayerResponse.Length; i++) { PlayerResponse[i] = null; NumberOpen[i] = 0; }
        // Сбрасываем количество открытых букв
        OpenLetters = 0;

        // Если необходимо, обновляем текстовое поле ответа
        if (updateFieldAnswer) UpdateAnswer();

        // Убираем красную обводку текста
        if (outline) outline.enabled = false;
    }

    // Проверка буквенного ответа
    public void ComparisonAnswers(bool yourAnswer)
    {
        // Если массив ответа полностью совпадает с массивом выбранных букв
        if (questions.Tasks.questions[questions.Progress - 1].answer.SequenceEqual(PlayerResponse))
        {
            // Сообщаем о правильном ответе
            CorrectAnswer(yourAnswer);
            // Выводим полное описание ответа
            Invoke("ShowDescription", 0.2f);
        }
        // Если ответ не правильный
        else if (PlayerResponse.Last() != null)
        {
            // Отображаем красную обводку
            outline.enabled = true;
            // Увеличиваем количество ошибок в викторине
            PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
        }
    }

    // Проверка варианта ответа
    public void ComparisonAnswers(int number)
    {
        // Если выбранный вариант совпадает с правильным
        if (questions.Tasks.questions[questions.Progress - 1].correct == number)
        {
            // Сообщаем о правильном ответе
            CorrectAnswer();
            // Выводим полное описание ответа
            Invoke("ShowDescription", 0.2f);
        }
        else
        {
            // Иначе начисляем штраф и обновляем статистику монет
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 50);
            statistics.UpdateCoins(true);

            // Увеличиваем количество ошибок в викторине
            PlayerPrefs.SetInt("countries-error", PlayerPrefs.GetInt("countries-error") + 1);
        }
    }

    // Правильный ответ на вопрос
    private void CorrectAnswer(bool yourAnswer = true)
    {
        // Скрываем все лишние объекты на сцене
        for (int i = 0; i < hiddenObjects.Length; i++) { hiddenObjects[i].SetActive(false); }

        // Выводим полный ответ в текстовое поле
        textAnswer.text = questions.Tasks.questions[questions.Progress - 1].full_answer;

        // Если игрок ответил самостоятельно, проигрываем победный эффект
        if (yourAnswer) particle.Play();

        // Увеличиваем прогресс викторины
        IncreaseProgress(yourAnswer);
    }

    // Увеличение прогресса викторины
    private void IncreaseProgress(bool yourAnswer)
    {
        // Если пользователь ответил самостоятельно
        if (yourAnswer)
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
        questions.Sets.arraySets[Categories.category]++;
        // Сохраняем новое значение
        PlayerPrefs.SetString("sets", JsonUtility.ToJson(questions.Sets));

        // Активируем кнопку для перехода к следующему вопросу
        nextQuestion.interactable = true;
    }

    // Получение правильного ответа (пропуск вопроса)
    public void GetRightAnswer()
    {
        // Устанавливаем в пользовательский ответ массив с правильным ответом
        PlayerResponse = questions.Tasks.questions[questions.Progress - 1].answer;
        // Вызываем проверку ответа (с указанием пропуска)
        ComparisonAnswers(false);
    }

    // Описание правильного ответа
    private void ShowDescription()
    {
        // Выводим описание правильного ответа в текстовое поле
        responseDescription.text = questions.Tasks.questions[questions.Progress - 1].description;
    }

    // Обновление задания
    public void TaskUpdate()
    {
        // Если сохраненный прогресс не превышает количество вопросов
        if (questions.Progress < questions.Tasks.questions.Length)
            // Перезагружаем сцену для обновления задания
            Camera.main.GetComponent<TransitionsInMenu>().RestartScene();
        else
        {
            // Иначе начисляем бонус за пройденную категорию
            PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + 500);
            // Переходим в список пройденных вопросов
            Camera.main.GetComponent<TransitionsInMenu>().GoToScene(4);
        }
    }
}