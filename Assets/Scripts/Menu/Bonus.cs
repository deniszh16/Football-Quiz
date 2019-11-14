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
        CheckBonus(); 
    }

    /// <summary>Проверка доступности бонуса</summary>
    public void CheckBonus()
    {
        // Если доступны просмотры бонусной рекламы
        if (PlayerPrefs.GetInt("bonus") > 0)
        {
            // Активируем бонусную кнопку
            bonus.gameObject.SetActive(true);
            // Активируем эффект сияния
            effect.SetActive(true);
        }
    }

    /// <summary>Уменьшение количества доступных просмотров видеорекламы</summary>
    public void DecreaseViews()
    {
        // Уменьшаем количество просмотров видеорекламы с вознаграждением
        PlayerPrefs.SetInt("bonus", PlayerPrefs.GetInt("bonus") - 1);

        CheckBonus();
    }
}