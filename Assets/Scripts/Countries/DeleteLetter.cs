using UnityEngine;

public class DeleteLetter : MonoBehaviour
{
    [Header("Компонент ответа")]
    [SerializeField] private AnswerCountries answer;

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    private void Awake()
    {
        answer = answer.GetComponent<AnswerCountries>();
    }

    /// <summary>Нажатие на кнопку удаления буквы</summary>
    public void PressDelete()
    {
        // Если есть открытые буквы
        if (answer.QuantityLetters - 1 >= 0)
        {
            // Стираем последнюю букву
            answer.PlayerResponse[answer.QuantityLetters - 1] = null;

            // Обновляем поле ответа
            answer.UpdateAnswer();

            // Отображаем скрытую кнопку буквы
            letters.transform.GetChild(answer.NumbersLetters[answer.QuantityLetters - 1]).gameObject.SetActive(true);

            // Уменьшаем количество открытых букв
            answer.QuantityLetters--;

            // Если отображалась обводка ответа, убираем её
            if (answer.Outline.enabled) answer.Outline.enabled = false;
        }
    }
}