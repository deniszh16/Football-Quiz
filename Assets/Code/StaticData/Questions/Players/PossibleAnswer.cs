using System;
using UnityEngine;

namespace Code.StaticData.Questions.Players
{
    [Serializable]
    public class PossibleAnswer
    {
        [Header("Спрайт варианта")]
        public Sprite Variant;
        
        [Header("Правильный ответ")]
        public bool CorrectAnswer;
    }
}