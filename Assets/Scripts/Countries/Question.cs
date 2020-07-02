using UnityEngine;
using UnityEngine.UI;

namespace Cubra.Countries
{
    public class Question : MonoBehaviour
    {
        private Text _question;

        private void Awake()
        {
            _question = GetComponent<Text>();
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