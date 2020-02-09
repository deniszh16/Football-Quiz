using UnityEngine;
using UnityEngine.UI;

public class TaskButton : MonoBehaviour
{
    // Ссылка на компонент вопросов
    private TasksCountries questions;

    private void Awake()
    {
        questions = Camera.main.GetComponent<TasksCountries>();
    }

    /// <summary>
    /// Выбор буквы
    /// </summary>
    /// <param name="number">Номер буквы</param>
    public void ChooseLetter(int number)
    {
        for (int i = 0; i < questions.Answer.PlayerResponse.Length; i++)
        {
            // Если в массиве выбранных букв есть пустая ячейка
            if (questions.Answer.PlayerResponse[i] == null)
            {
                // Записываем выбранную букву в массив ответа
                questions.Answer.PlayerResponse[i] = GetButtonText(number);
                // Увеличиваем количество открытых букв
                questions.Answer.QuantityLetters++;
                // Запоминаем номер выбранной буквы
                questions.Answer.NumbersLetters[questions.Answer.QuantityLetters - 1] = number;

                // Скрываем выбранную букву
                transform.GetChild(number).gameObject.SetActive(false);

                // Обновляем текстовое поле ответа
                questions.Answer.UpdateResponseField();
                // Выполняем проверку правильного ответа
                questions.Answer.ComparisonAnswers(true);
                break;
            }
        }
    }

    /// <summary>
    /// Получение текста с нажатой кнопки
    /// </summary>
    /// <param name="number">Номер кнопки</param>
    private string GetButtonText(int number)
    {
        return transform.GetChild(number).GetComponentInChildren<Text>().text.ToLower();
    }

    /// <summary>
    /// Выбор варианта ответа
    /// </summary>
    /// <param name="number">Номер кнопки</param>
    public void ChooseOption(int number)
    {
        // Скрываем вариант ответа
        transform.GetChild(number - 1).gameObject.SetActive(false);

        // Выполняем проверку ответа
        questions.Answer.ComparisonAnswers(number);
    }
}