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
        CheckDailyBonus(); 
    }

    /// <summary>Проверка доступности бонуса</summary>
    private void CheckDailyBonus()
    {
        // Если дрступна бонусная реклама
        if (PlayerPrefs.GetInt("bonus") > 0)
        {
            // Активируем кнопку и эффект
            bonus.gameObject.SetActive(true);
            effect.SetActive(true);
        }
    }

    /// <summary>Уменьшение количества доступных просмотров бонусной видеорекламы</summary>
    public void DecreaseViews()
    {
        PlayerPrefs.SetInt("bonus", PlayerPrefs.GetInt("bonus") - 1);
        CheckDailyBonus();
    }
}