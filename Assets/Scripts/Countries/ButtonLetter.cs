using UnityEngine;
using UnityEngine.UI;

public class ButtonLetter : MonoBehaviour
{
    [Header("Номер кнопки")]
    [SerializeField] private int number;
    
    private Text letter;
    private TasksCountries questions;

    private void Awake()
    {
        letter = GetComponentInChildren<Text>();
        questions = Camera.main.GetComponent<TasksCountries>();
    }

    // Нажатие на кнопку
    public void PushButton()
    {
        // Переборка массива с пользовательским ответом
        for (int i = 0; i < questions.Answer.PlayerResponse.Length; i++)
        {
            // Если есть незаполненная ячейка
            if (questions.Answer.PlayerResponse[i] == null)
            {
                // Записываем текущую букву в массив
                questions.Answer.PlayerResponse[i] = letter.text.ToLower();
                // Увеличиваем количество открытых букв
                questions.Answer.OpenLetters++;
                // Запоминаем номер буквы (если понадобится удаление)
                questions.Answer.NumberOpen[questions.Answer.OpenLetters - 1] = number;

                // Скрываем букву
                gameObject.SetActive(false);
                // Обновляем ответ в текстовом поле
                questions.Answer.UpdateAnswer();

                // Выполняем проверку правильного ответа
                questions.Answer.ComparisonAnswers(true);
                break;
            }
        }
    }
}