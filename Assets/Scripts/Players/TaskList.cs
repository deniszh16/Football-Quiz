using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Players
{
    public class TaskList : FileProcessing
    {
        [Header("Перечисление вопросов")]
        [SerializeField] private TextMeshProUGUI _questions;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        private PlayersHelpers _playersHelpers;

        private void Awake()
        {
            _playersHelpers = new PlayersHelpers();

            // Обрабатываем json файл и записываем в переменную
            string jsonString = ReadJsonFile("players-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _playersHelpers, jsonString);
        }

        private void Start()
        {
            // Выводим все задания из категории
            for (int i = 0; i < _playersHelpers.PhotoTasks.Length; i++)
            {
                _questions.text += IndentsHelpers.LineBreak(1) + _playersHelpers.PhotoTasks[i].Question + IndentsHelpers.LineBreak(2);
                _questions.text += "Ответ: " + _playersHelpers.PhotoTasks[i].Description + IndentsHelpers.LineBreak(1);
                _questions.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}