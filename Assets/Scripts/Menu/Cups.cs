using UnityEngine;
using UnityEngine.UI;

public class Cups : MonoBehaviour
{
    [Header("Массив кубков")]
    [SerializeField] private GameObject[] cups;

    [Header("Прогресс для открытия")]
    [SerializeField] private int[] progress;

    [Header("Статистика кубков")]
    [SerializeField] private Text percent;

    // Общий счет ответов
    private int totalScore;

    private void Awake()
    {
        // Подсчет общего количества правильных ответов
        totalScore = PlayerPrefs.GetInt("countries-answer") + PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers");
    }

    private void Start()
    {
        // Процент прохождения
        int percent = 0;

        for (int i = 0; i < cups.Length; i++)
        {
            // Если итоговый счет превышает необходимый прогресс
            if (progress[i] <= totalScore)
            {
                // Получаем графический компонент и убираем прозрачность
                cups[i].GetComponent<Image>().color = Color.white;
                // Увеличиваем процент прохождения
                percent +=15;
            } 
        }

        // Выводим проценты прохождения викторины (если процент больше ста, округляем до сотни)
        this.percent.text = "Выигранные кубки (" + (percent > 100 ? "100" : percent.ToString()) + "%)";
    }
}