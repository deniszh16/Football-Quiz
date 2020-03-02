using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    // Ссылки на компоненты
    private GameObject effect;
    private Button bonus;

    private void Awake()
    {
        effect = transform.GetChild(0).gameObject;
        bonus = transform.GetChild(1).GetComponent<Button>();
    }

    private void Start()
    {
        CheckDailyBonus();
    }

    /// <summary>
    /// Проверка количества доступных бонусов
    /// </summary>
    public void CheckDailyBonus()
    {
        // Если доступна бонусная реклама
        if (PlayerPrefs.GetInt("bonus") > 0)
        {
            // Активируем кнопку и эффект
            bonus.gameObject.SetActive(true);
            effect.SetActive(true);
        }
    }

    /// <summary>
    /// Уменьшение количества бонусов
    /// </summary>
    public void DecreaseViews()
    {
        PlayerPrefs.SetInt("bonus", PlayerPrefs.GetInt("bonus") - 1);
        CheckDailyBonus();
    }
}