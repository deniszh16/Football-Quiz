using UnityEngine;
using UnityEngine.UI;

public class Cups : MonoBehaviour
{
    [Header("Кубки за прогресс")]
    [SerializeField] private GameObject[] cups;

    [Header("Прогресс для открытия")]
    [SerializeField] private int[] progress;

    [Header("Текст с прогрессом")]
    [SerializeField] private Text percent;

    private void Start()
    {
        // Процент прохождения
        var percentagPeassing = 0;

        // Общий прогресс по викторинам
        var overallProgress = PlayerPrefs.GetInt("countries-answer") + PlayerPrefs.GetInt("players") + PlayerPrefs.GetInt("trainers");

        for (int i = 0; i < cups.Length; i++)
        {
            // Если общий прогресс превышает значение для открытия
            if (progress[i] <= overallProgress)
            {
                // Убираем прозрачность у кубка
                cups[i].GetComponent<Image>().color = Color.white;

                // Увеличиваем процент прохождения
                percentagPeassing += 15;
            }
        }

        // Выводим проценты прохождения викторины
        percent.text = "Выигранные кубки (" + (percentagPeassing > 100 ? "100" : percentagPeassing.ToString()) + "%)";
    }
}