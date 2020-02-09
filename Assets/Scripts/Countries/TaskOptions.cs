using UnityEngine;
using UnityEngine.UI;

public class TaskOptions : MonoBehaviour
{
    [Header("Варианты задания")]
    [SerializeField] private GameObject[] options;

    // Ссылка на компонент вопросов
    private TasksCountries questions;

    private void Awake()
    {
        questions = Camera.main.GetComponent<TasksCountries>();
    }

    private void Start()
    {
        // Если в массиве больше трех кнопок, расставляем буквы
        if (options.Length > 3) LettersArrange();
        // Иначе расставляем варианты ответов
        else VariantsArrange();     
    }

    /// <summary>
    /// Расстановка букв из задания
    /// </summary>
    private void LettersArrange()
    {
        for (int i = 0; i < options.Length; i++)
            // Получаем текстовый компонент у кнопки и устанавливаем букву из массива заданий
            options[i].GetComponentInChildren<Text>().text = questions.Tasks.TaskItems[questions.Progress - 1].Letters[i];
    }

    /// <summary>
    /// Растановка вариантов ответа
    /// </summary>
    private void VariantsArrange()
    {
        for (int i = 0; i < options.Length; i++)
            options[i].GetComponentInChildren<Text>().text = questions.Tasks.TaskItems[questions.Progress - 1].Options[i];
    }
}