using System;
using UnityEngine;

namespace StaticData.Questions.Players
{
    [Serializable]
    public class Question
    {
        [Header("Вопрос")]
        public string Task;
        
        [Header("Варианты ответа")]
        public PossibleAnswer[] Variants;
        
        [Header("Количество ответов")]
        public int NumberOfAnswers;
        
        [Header("Количество попыток")]
        public int Attempts;
        
        [Header("Описание ответа")]
        public string Description;
    }
}