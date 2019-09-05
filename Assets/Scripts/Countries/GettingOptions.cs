using UnityEngine;
using UnityEngine.UI;

public class GettingOptions : MonoBehaviour
{
    [Header("Набор вариантов")]
    [SerializeField] private GameObject[] options;

    // Ссылка на компонент вопросов
    private TasksCountries questions;

    private void Awake() { questions = Camera.main.GetComponent<TasksCountries>(); }

    private void Start() { OptionsArrange(); }

    // Растановка вариантов ответа
    private void OptionsArrange()
    {
        for (int i = 0; i < options.Length; i++)
        {
            // Получение текстового компонента у дочернего объекта и установка варианта из массива
            options[i].GetComponentInChildren<Text>().text = questions.Tasks.questions[questions.Progress - 1].options[i];
        }
    }
}
