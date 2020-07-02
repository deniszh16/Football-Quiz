using UnityEngine;

namespace Cubra.Countries
{
    public class ChoiceOptions : MonoBehaviour
    {
        private TasksCountries _tasksCountries;

        private void Awake()
        {
            _tasksCountries = Camera.main.GetComponent<TasksCountries>();
        }

        /// <summary>
        /// Выбор варианта ответа
        /// </summary>
        /// <param name="number">номер кнопки</param>
        public void ChooseOption(int number)
        {
            // Скрываем вариант ответа
            transform.GetChild(number - 1).gameObject.SetActive(false);

            // Проверяем ответ
            _tasksCountries.Answer.CheckPlayerAnswer(number);
        }
    }
}