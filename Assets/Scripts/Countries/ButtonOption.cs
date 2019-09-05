using UnityEngine;

public class ButtonOption : MonoBehaviour
{
    [Header("Номер кнопки")]
    [SerializeField] private int number;

    private TasksCountries questions;

    private void Awake() { questions = Camera.main.GetComponent<TasksCountries>(); }

    // Нажатие на кнопку
    public void PushButton()
    {
        // Скрываем кнопку
        gameObject.SetActive(false);
        // Проверяем ответ
        questions.Answer.ComparisonAnswers(number);
    }
}