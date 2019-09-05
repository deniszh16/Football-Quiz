using UnityEngine;
using UnityEngine.UI;

public class TipsTeams : MonoBehaviour
{
    [Header("Подсказка по командам")]
    [SerializeField] private Text team;

    private Statistics statistics;
    private ImageList imageList;

    private void Awake()
    {
        imageList = GameObject.FindGameObjectWithTag("Tasks").GetComponent<ImageList>();
        statistics = Camera.main.GetComponent<Statistics>();
    }

    // Вывод названия команды
    public void ShowTeam()
    {
        // Вычитаем стоимость подсказки и обновляем статистику
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") - 50);
        statistics.UpdateCoins(true);

        // Устанавливаем текст с названием команды
        team.text = imageList.teams[PlayerPrefs.GetInt(Modes.category)];

        // Увеличиваем количество использованных подсказок
        PlayerPrefs.SetInt(Modes.category + "-tips", PlayerPrefs.GetInt(Modes.category + "-tips") + 1);
    }
}