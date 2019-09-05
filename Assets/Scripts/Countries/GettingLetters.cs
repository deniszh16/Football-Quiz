using UnityEngine;
using UnityEngine.UI;

public class GettingLetters : MonoBehaviour
{
    [Header("Набор из 12 букв")]
    [SerializeField] private GameObject[] letters;

    // Ссылка на компонент вопросов
    private TasksCountries questions;

    private void Awake() { questions = Camera.main.GetComponent<TasksCountries>(); }

    private void Start() { LettersArrange(); }

    // Расстановка букв из задания
    private void LettersArrange()
    {
        for (int i = 0; i < letters.Length; i++)
        {
            // Получение текстового компонента у дочернего объекта, и установка буквы из массива
            letters[i].GetComponentInChildren<Text>().text = questions.Tasks.questions[questions.Progress - 1].letters[i];
        }
    }
}