using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;

namespace Cubra.Facts
{
    public class TaskList : FileProcessing
    {
        [Header("Перечисление вопросов")]
        [SerializeField] private Text _questions;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        private FactsHelper _factsHelper;

        private void Awake()
        {
            _factsHelper = new FactsHelper();

            // Обрабатываем json файл и записываем в переменную
            string jsonString = ReadJsonFile("facts-" + Sets.Category);
            // Преобразовываем строку в объект
            ConvertToObject(ref _factsHelper, jsonString);
        }

        private void Start()
        {
            for (int i = 0; i < _factsHelper.Facts.Length; i++)
            {
                _questions.text += IndentsHelpers.LineBreak(1) + _factsHelper.Facts[i].Question + IndentsHelpers.LineBreak(2);
                _questions.text += "Ответ: " + (_factsHelper.Facts[i].Answer ? "Правда" : "Неправда") + IndentsHelpers.LineBreak(1);

                // Создаем отделяющую черту
                _questions.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);   
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}