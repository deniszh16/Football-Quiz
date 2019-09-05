using UnityEngine;
using UnityEngine.UI;

public class ShowQuestion : MonoBehaviour
{
    private Text text;
    private TasksCountries questions;

    private void Awake()
    {
        text = GetComponent<Text>();
        questions = Camera.main.GetComponent<TasksCountries>();
    }

    // Установка вопроса при старте
    private void Start() { if (questions.Progress - 1 < questions.Tasks.questions.Length) GetQuestion(); }

    // Установка текста из массива вопросов
    public void GetQuestion() { text.text = questions.Tasks.questions[questions.Progress - 1].question; }
}