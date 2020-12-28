using UnityEngine;
using TMPro;

namespace Cubra.Countries
{
    public class Question : MonoBehaviour
    {
        private TextMeshProUGUI _question;

        private void Awake()
        {
            _question = GetComponent<TextMeshProUGUI>();
        }

        /// <summary>
        /// Отображение вопроса
        /// </summary>
        /// <param name="question">текст вопроса</param>
        public void ShowQuestion(string question)
        {
            _question.text = question;
        }
    }
}