using UnityEngine;
using UnityEngine.UI;

namespace Cubra
{
    public class Cups : MonoBehaviour
    {
        [Header("Кубки за прогресс")]
        [SerializeField] private Image[] _cups;

        [Header("Прогресс для открытия")]
        [SerializeField] private int[] _progress;

        [Header("Проценты прохождения")]
        [SerializeField] private Text _percent;

        private void Start()
        {
            // Процент прохождения
            var percentages = 0;

            // Общий прогресс по викторинам (по странам, по игрокам и тренерам, по фактам)
            var overallProgress = PlayerPrefs.GetInt("countries-answer") + PlayerPrefs.GetInt("players")
                + PlayerPrefs.GetInt("trainers") + PlayerPrefs.GetInt("facts-answer");

            for (int i = 0; i < _cups.Length; i++)
            {
                // Если общий прогресс превышает кубковый
                if (_progress[i] <= overallProgress)
                {
                    _cups[i].color = Color.white;
                    percentages += 15;
                }
            }

            // Выводим процент прохождения викторины
            _percent.text = "Выигранные кубки (" + (percentages > 100 ? "100" : percentages.ToString()) + "%)";
        }
    }
}