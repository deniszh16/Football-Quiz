using UnityEngine;
using UnityEngine.UI;

public class Results : MonoBehaviour
{
    [Header("Количество заданий")]
    [SerializeField] private Tasks[] tasks;

    [Header("Панель скролла")]
    [SerializeField] private ScrollRect scroll;

    // Ссылка на текст скролла
    private Text results;

    private void Awake()
    {
        results = GetComponent<Text>();
        scroll = scroll.GetComponent<ScrollRect>();
    }

    private void Start()
    {
        // Статистика по викторине стран и турниров
        results.text += "Викторина по странам" + Indents.LineBreak(2);
        results.text += "Всего вопросов: " + GetStatistics(tasks[0]) + Indents.LineBreak(1);
        results.text += "Правильные ответы: " + PlayerPrefs.GetInt("countries-answer") + Indents.LineBreak(1);
        results.text += "Количество ошибок: " + PlayerPrefs.GetInt("countries-error") + Indents.LineBreak(1);
        results.text += "Количество подсказок: " + PlayerPrefs.GetInt("countries-tips") + Indents.LineBreak(1);
        results.text += "Пропуски вопросов: " + PlayerPrefs.GetInt("countries-pass") + Indents.LineBreak(1);
        results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("countries-answer") * 5) + Indents.LineBreak(2);

        // Отделяющая строка
        results.text += Indents.Underscore(20) + Indents.LineBreak(2);

        // Статистика по викторине с фотографиями
        results.text += "Викторина по игрокам" + Indents.LineBreak(2);
        results.text += "Всего заданий: " + GetStatistics(tasks[1]) + Indents.LineBreak(1);
        results.text += "Правильные ответы: " + (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers")) + Indents.LineBreak(1);
        results.text += "Количество ошибок: " + (PlayerPrefs.GetInt("players-errors") + PlayerPrefs.GetInt("trainers-errors")) + Indents.LineBreak(1);
        results.text += "Открытые фрагменты: " + (PlayerPrefs.GetInt("players-buttons") + PlayerPrefs.GetInt("trainers-buttons")) + Indents.LineBreak(1);
        results.text += "Количество подсказок: " + (PlayerPrefs.GetInt("players-tips") + PlayerPrefs.GetInt("trainers-tips")) + Indents.LineBreak(1);
        results.text += "Пропуски заданий: " + (PlayerPrefs.GetInt("players-pass") + PlayerPrefs.GetInt("trainers-pass")) + Indents.LineBreak(1);
        results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers")) *3 + Indents.LineBreak(2);

        // Отделяющая строка
        results.text += Indents.Underscore(20) + Indents.LineBreak(2);

        // Статистика по футбольным фактам
        results.text += "Викторина по фактам" + Indents.LineBreak(2);
        results.text += "Количество подборок: " + tasks[2].quantityLength + Indents.LineBreak(1);
        results.text += "Всего вопросов: " + GetStatistics(tasks[2]) + Indents.LineBreak(1);
        results.text += "Завершенные подборки: " + PlayerPrefs.GetInt("facts-quantity") + Indents.LineBreak(1);
        results.text += "Выигранные подборки: " + PlayerPrefs.GetInt("facts-victory") + Indents.LineBreak(1);
        results.text += "Правильные ответы: " + PlayerPrefs.GetInt("facts-answer") + Indents.LineBreak(1);
        results.text += "Количество ошибок: " + PlayerPrefs.GetInt("facts-errors") + Indents.LineBreak(1);
        results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("facts-answer") * 3) + Indents.LineBreak(2);

        // Отделяющая строка
        results.text += Indents.Underscore(20) + Indents.LineBreak(2);

        // Статистика по легендам
        results.text += "Футбольные легенды" + Indents.LineBreak(2);
        results.text += "Всего карточек: 44" + Indents.LineBreak(1);
        results.text += "Открытые карточки: " + PlayerPrefs.GetInt("legends-open") + Indents.LineBreak(1);
        results.text += "Потрачено монет: " + (PlayerPrefs.GetInt("legends-open") * 950) + Indents.LineBreak(1);

        // Поднимаем скролл в самый верх
        scroll.verticalNormalizedPosition = 1;
    }

    /// <summary>Получение общего количества заданий в викторинах (объект с заданиями)</summary>
    private string GetStatistics(IQuantity tasks)
    {
        // Количество заданий
        var quantity = 0;

        // Подсчитываем общее количество заданий
        for (int i = 0; i < tasks.quantityLength; i++)
        {
            quantity += tasks[i];
        }

        return quantity.ToString();
    }
}