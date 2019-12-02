using UnityEngine;

public class DeleteLetter : MonoBehaviour
{
    [Header("Компонент ответа")]
    [SerializeField] private AnswerCountries answer;

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    /// <summary>Нажатие на кнопку удаления буквы</summary>
    public void PressDelete()
    {
        // Если есть открытые буквы
        if (answer.QuantityLetters - 1 >= 0)
        {
            // Стираем последнюю букву
            answer.PlayerResponse[answer.QuantityLetters - 1] = null;
            // Обновляем текстовое поле ответа
            answer.UpdateResponseField();

            // Отображаем скрытую кнопку буквы
            letters.transform.GetChild(answer.NumbersLetters[answer.QuantityLetters - 1]).gameObject.SetActive(true);
            // Уменьшаем количество открытых букв
            answer.QuantityLetters--;

            // Убираем красную обводку ответа
            answer.ChangeOutlineText(false);
        }
    }
}