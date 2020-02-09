using UnityEngine;
using UnityEngine.UI;

public class Cups : MonoBehaviour
{
    [Header("Кубки за прогресс")]
    [SerializeField] private Image[] cups;

    [Header("Прогресс для открытия")]
    [SerializeField] private int[] progress;

    [Header("Текст статистики")]
    [SerializeField] private Text percent;

    private void Start()
    {
        // Процент прохождения
        var percentagPeassing = 0;

        // Общий прогресс по викторинам (по странам, по игрокам и тренерам, по фактам)
        var overallProgress = PlayerPrefs.GetInt("countries-answer") + PlayerPrefs.GetInt("players") 
            + PlayerPrefs.GetInt("trainers") + PlayerPrefs.GetInt("facts-answer");

        for (int i = 0; i < cups.Length; i++)
        {
            // Если общий прогресс превышает кубковый
            if (progress[i] <= overallProgress)
            {
                // Убираем прозрачность у кубка
                cups[i].color = Color.white;

                // Увеличиваем процент прохождения
                percentagPeassing += 15;
            }
        }

        // Выводим процент прохождения викторины
        percent.text = "Выигранные кубки (" + (percentagPeassing > 100 ? "100" : percentagPeassing.ToString()) + "%)";
    }
}