using UnityEngine;
using UnityEngine.UI;

public class QuizResults : MonoBehaviour
{
    [Header("Количество заданий")]
    [SerializeField] private NumberTasks[] tasks;

    [Header("Панель скролла")]
    [SerializeField] private ScrollRect scroll;

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

        // Создание отделяющей строки
        results.text += Indents.Underscore(16) + Indents.LineBreak(2);

        // Статистика по викторине с фотографиями
        results.text += "Викторина по игрокам" + Indents.LineBreak(2);
        results.text += "Всего заданий: " + GetStatistics(tasks[1]) + Indents.LineBreak(1);
        results.text += "Правильные ответы: " + (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers")) + Indents.LineBreak(1);
        results.text += "Количество ошибок: " + (PlayerPrefs.GetInt("players-errors") + PlayerPrefs.GetInt("trainers-errors")) + Indents.LineBreak(1);
        results.text += "Открытые фрагменты: " + (PlayerPrefs.GetInt("players-buttons") + PlayerPrefs.GetInt("trainers-buttons")) + Indents.LineBreak(1);
        results.text += "Количество подсказок: " + (PlayerPrefs.GetInt("players-tips") + PlayerPrefs.GetInt("trainers-tips")) + Indents.LineBreak(1);
        results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers")) *3 + Indents.LineBreak(2);

        // Создание отделяющей строки
        results.text += Indents.Underscore(16) + Indents.LineBreak(2);

        // Статистика по онлайн викторине
        results.text += "Футбольные баттлы" + Indents.LineBreak(2);
        results.text += "Всего батлов: 0" + Indents.LineBreak(1);
        results.text += "Выигранные кубки: 0" + Indents.LineBreak(1);
        results.text += "Правильные ответы: 0" + Indents.LineBreak(1);
        results.text += "Количество ошибок: 0" + Indents.LineBreak(1);
        results.text += "Заработанные очки: 0" + Indents.LineBreak(2);

        // Создание отделяющей строки
        results.text += Indents.Underscore(16) + Indents.LineBreak(2);

        // Статистика по легендам
        results.text += "Футбольные легенды" + Indents.LineBreak(2);
        results.text += "Всего карточек: 44" + Indents.LineBreak(1);
        results.text += "Открытые карточки: " + PlayerPrefs.GetInt("legends-open") + Indents.LineBreak(1);
        results.text += "Потрачено монет: " + (PlayerPrefs.GetInt("legends-open") * 950) + Indents.LineBreak(1);

        // Поднимаем скролл в самый верх
        scroll.verticalNormalizedPosition = 1;
    }

    // Получение статистики из викторин
    private string GetStatistics(IQuantity obj)
    {
        // Количество заданий
        int quantity = 0;
        // Подсчитываем общее количество заданий и возвращаем результат
        for (int i = 0; i < obj.quantityLength; i++) { quantity += obj[i]; }
        return quantity.ToString();
    }
}