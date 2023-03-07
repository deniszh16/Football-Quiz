using System;
using UnityEngine;

namespace Code.StaticData.Questions.Countries
{
    [Serializable]
    public class Question
    {
        [Header("Тип вопроса")]
        public TypeQuestions TypeQuestion;
        
        [Space]
        [Header("Вопрос")]
        public string Task;
        [Header("Буквы задания")]
        public string[] Letters;
        [Header("Буквы ответа")]
        public string[] Answer;
        
        [Space]
        [Header("Номер ответа")]
        [Range(1, 3)]
        public int NumberOfAnswer;
        [Header("Варианты задания")]
        public string[] Variants;
        
        [Space]
        [Header("Полный ответ")]
        public string FullAnswer;
        [Header("Описание ответа")]
        public string Description;
    }
}