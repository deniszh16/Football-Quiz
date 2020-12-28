using UnityEngine;
using TMPro;

namespace Cubra.Countries
{
    public class ChoiceLetters : MonoBehaviour
    {
        private TasksCountries _tasksCountries;

        private void Awake()
        {
            _tasksCountries = Camera.main.GetComponent<TasksCountries>();
        }

        /// <summary>
        /// Выбор буквы для ответа
        /// </summary>
        /// <param name="number">Номер буквы</param>
        public void ChooseLetter(int number)
        {
            // Получаем выбранную букву
            var letter = GetButtonText(number);

            for (int i = 0; i < _tasksCountries.Answer.AnswerLength; i++)
            {
                // Если удалось записать букву в ответ
                if (_tasksCountries.Answer.WriteLetterInAnswer(i, number, letter))
                {
                    // Скрываем выбранную букву
                    transform.GetChild(number).gameObject.SetActive(false);
                    break;
                }
            }
        }

        /// <summary>
        /// Получение буквы с нажатой кнопки
        /// </summary>
        /// <param name="number">номер кнопки</param>
        private string GetButtonText(int number)
        {
            return transform.GetChild(number).GetComponentInChildren<TextMeshProUGUI>().text.ToLower();
        }
    }
}