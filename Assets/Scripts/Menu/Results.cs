using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra
{
    public class Results : MonoBehaviour
    {
        [Header("Списки заданий")]
        [SerializeField] private Tasks[] _tasks;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Текст результатов")]
        [SerializeField] private TextMeshProUGUI _results;

        private void Start()
        {
            // Статистика по викторине стран и турниров
            _results.text += "Викторина по странам" + IndentsHelpers.LineBreak(2);
            _results.text += "Всего вопросов: " + GetStatistics(_tasks[0]) + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + PlayerPrefs.GetInt("countries-answer") + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + PlayerPrefs.GetInt("countries-error") + IndentsHelpers.LineBreak(1);
            _results.text += "Количество подсказок: " + PlayerPrefs.GetInt("countries-tips") + IndentsHelpers.LineBreak(1);
            _results.text += "Пропуски вопросов: " + PlayerPrefs.GetInt("countries-pass") + IndentsHelpers.LineBreak(1);
            _results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("countries-answer") * 5) + IndentsHelpers.LineBreak(2);

            // Отделяющая строка
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);

            // Статистика по футбольным фактам
            _results.text += "Викторина по фактам" + IndentsHelpers.LineBreak(2);
            _results.text += "Количество подборок: " + _tasks[1].QuantityCategories + IndentsHelpers.LineBreak(1);
            _results.text += "Всего вопросов: " + GetStatistics(_tasks[1]) + IndentsHelpers.LineBreak(1);
            _results.text += "Завершенные подборки: " + PlayerPrefs.GetInt("facts-quantity") + IndentsHelpers.LineBreak(1);
            _results.text += "Выигранные подборки: " + PlayerPrefs.GetInt("facts-victory") + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + PlayerPrefs.GetInt("facts-answer") + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + PlayerPrefs.GetInt("facts-errors") + IndentsHelpers.LineBreak(1);
            _results.text += "Заработанные очки: " + (PlayerPrefs.GetInt("facts-answer") * 3) + IndentsHelpers.LineBreak(2);

            // Отделяющая строка
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);

            _results.text += "Викторина по фотографиям" + IndentsHelpers.LineBreak(2);
            _results.text += "Количество заданий: " + GetStatistics(_tasks[2]) + IndentsHelpers.LineBreak(1);
            _results.text += "Успешные задания: " + PlayerPrefs.GetInt("photo-quiz-successfully") + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + PlayerPrefs.GetInt("photo-quiz-answer") + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + PlayerPrefs.GetInt("photo-quiz-errors") + IndentsHelpers.LineBreak(2);

            // Отделяющая строка
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);

            // Статистика по легендам
            _results.text += "Футбольные легенды" + IndentsHelpers.LineBreak(2);
            _results.text += "Всего карточек: 44" + IndentsHelpers.LineBreak(1);
            _results.text += "Открытые карточки: " + PlayerPrefs.GetInt("legends-open") + IndentsHelpers.LineBreak(1);
            _results.text += "Потрачено монет: " + (PlayerPrefs.GetInt("legends-open") * 950) + IndentsHelpers.LineBreak(1);

            _scrollRect.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// Получение общего количества заданий в викторинах
        /// </summary>
        /// <param name="tasks">объект категорий</param>
        /// <returns>количество заданий</returns>
        private string GetStatistics(IQuantity tasks)
        {
            var quantity = 0;

            // Подсчитываем общее количество заданий
            for (int i = 0; i < tasks.QuantityCategories; i++)
                quantity += tasks[i];

            return quantity.ToString();
        }
    }
}