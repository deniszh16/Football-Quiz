using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Countries
{
    public class TaskList : FileProcessing
    {
        [Header("Перечисление вопросов")]
        [SerializeField] private TextMeshProUGUI _questions;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        private QuestionsHelpers _questionsHelpers;

        private void Awake()
        {
            _questionsHelpers = new QuestionsHelpers();

            // Обрабатываем json файл и записываем в переменную
            string jsonString = ReadJsonFile("category-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _questionsHelpers, jsonString);
        }

        private void Start()
        {
            // Выводим все задания из категории
            for (int i = 0; i < _questionsHelpers.TaskItems.Length; i++)
            {
                _questions.text += IndentsHelpers.LineBreak(1) + _questionsHelpers.TaskItems[i].Question + IndentsHelpers.LineBreak(2);
                _questions.text += "Ответ: " + _questionsHelpers.TaskItems[i].FullAnswer + IndentsHelpers.LineBreak(1);

                // Создаем отделяющую черту
                _questions.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}