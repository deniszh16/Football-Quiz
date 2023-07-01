using System.Collections.Generic;
using UnityEngine;

namespace StaticData.Questions.Countries
{
    [CreateAssetMenu(fileName ="CountriesStaticData", menuName = "StaticData/Countries Static Data")]
    public class CountriesStaticData : ScriptableObject
    {
        [Header("Номер категории")]
        public int CategoryNumber;
        
        [Space]
        [Header("Вопросы категории")]
        public List<Question> Questions;
    }
}