using System;
using UnityEngine;

namespace DZGames.Football.StaticData.Facts
{
    [Serializable]
    public class Fact
    {
        [Header("Вопрос")]
        public string Question;
        
        [Header("Ответ")]
        public bool Answer;
        
        [Header("Описание ответа")]
        public string Description;
    }
}