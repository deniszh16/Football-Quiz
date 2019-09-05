using UnityEngine;
using UnityEngine.UI;

public class DeleteLetter : MonoBehaviour
{
    [Header("Компонент ответа")]
    [SerializeField] private AnswerCountries answer;

    private Outline outlineText;

    [Header("Панель букв")]
    [SerializeField] private GameObject letters;

    private void Awake()
    {
        answer = answer.GetComponent<AnswerCountries>();
        outlineText = answer.GetComponent<Outline>();
    }

    // Нажатие на кнопку удаления буквы
    public void PressDeleteButton()
    {
        // Если есть открытые буквы
        if (answer.OpenLetters - 1 >= 0)
        {
            // Стираем последнюю букву
            answer.PlayerResponse[answer.OpenLetters - 1] = null;

            // Обновляем поле ответа
            answer.UpdateAnswer();

            // Отображаем скрытую кнопку буквы
            letters.transform.GetChild(answer.NumberOpen[answer.OpenLetters - 1]).gameObject.SetActive(true);

            // Уменьшаем количество открытых букв
            answer.OpenLetters--;

            // Если отображалась красная обводка, убираем её
            if (outlineText.enabled) outlineText.enabled = false;
        }
    }
}