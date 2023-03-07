using System.Collections.Generic;
using UnityEngine;

namespace Code.StaticData.Questions.Countries
{
    [CreateAssetMenu(fileName ="QuestionStaticData", menuName = "StaticData/Question Static Data")]
    public class CountriesStaticData : ScriptableObject
    {
        [Header("Номер категории")]
        public int CategoryNumber;
        
        [Space]
        [Header("Вопросы категории")]
        public List<Question> Questions;
    }
}