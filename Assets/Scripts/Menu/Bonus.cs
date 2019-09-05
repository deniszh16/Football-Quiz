using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    [Header("Бонусная кнопка")]
    [SerializeField] private Button bonus;

    [Header("Эффект сияния")]
    [SerializeField] private GameObject effect;

    private void Start()
    {
        // Если доступен интернет, проверяем доступность бонуса
        if (Application.internetReachability != NetworkReachability.NotReachable) CheckBonus();
    }

    // Проверка бонуса
    public void CheckBonus()
    {
        // Если доступны просмотры бонусной рекламы
        if (PlayerPrefs.GetInt("bonus") > 0)
        {
            // Активируем кнопку и эффект сияния
            bonus.gameObject.SetActive(true);
            effect.SetActive(true);
        }
    }

    // Уменьшение просмотров
    public void DecreaseViews()
    {
        // Уменьшаем количество просмотров видеорекламы с вознаграждением
        PlayerPrefs.SetInt("bonus", PlayerPrefs.GetInt("bonus") - 1);
        // Проверяем доступные просмотры
        CheckBonus();
    }
}