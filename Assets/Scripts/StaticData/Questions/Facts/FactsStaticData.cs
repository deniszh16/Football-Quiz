using System.Collections.Generic;
using UnityEngine;

namespace DZGames.Football.StaticData.Facts
{
    [CreateAssetMenu(fileName = "FactsStaticData", menuName = "StaticData/Facts Static Data")]
    public class FactsStaticData : ScriptableObject
    {
        [Header("Номер категории")]
        public int CategoryNumber;
        
        [Space]
        [Header("Вопросы категории")]
        public List<Fact> Questions;
    }
}