using UnityEngine;
using UnityEngine.UI;

public class Question : MonoBehaviour
{
    // Ссылки на компоненты
    private Text textComponent;
    private TasksCountries questions;

    private void Awake()
    {
        textComponent = GetComponent<Text>();
        questions = Camera.main.GetComponent<TasksCountries>();
    }

    private void Start()
    {
        // Если прогресс не превышает количество вопросов в массиве
        if ((questions.Progress - 1) < questions.Tasks.TaskItems.Length)
            // Устанавливаем вопрос
            GetQuestion();
    }

    /// <summary>Вывод вопроса из массива</summary>
    public void GetQuestion()
    {
        textComponent.text = questions.Tasks.TaskItems[questions.Progress - 1].Question;
    }
}