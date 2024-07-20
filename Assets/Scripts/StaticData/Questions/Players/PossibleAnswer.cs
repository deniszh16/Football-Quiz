using System;
using UnityEngine;

namespace DZGames.Football.StaticData.Players
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