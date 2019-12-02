using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [Header("Тексты статистики")]
    [SerializeField] private Text[] texts;

    // Ссылка на аниматор текста
    private Animator animator;

    private void Awake()
    {
        animator = texts[1].transform.parent.GetComponent<Animator>();
    }

    private void Start()
    {
        UpdateTotalScore();
        UpdateTotalCoins(false);
    }

    /// <summary>Изменение общего счета (значение)</summary>
    public void ChangeTotalScore(int value)
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + value);
        UpdateTotalScore();
    }

    /// <summary>Обновление статистики общего счета</summary>
    public void UpdateTotalScore()
    {
        texts[0].text = PlayerPrefs.GetInt("score").ToString();
    }

    /// <summary>Изменение количества монет (значение)</summary>
    public void ChangeTotalCoins(int value)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + value);

        // Если монеты ушли в минус, обнуляем общее количество
        if (PlayerPrefs.GetInt("coins") < 0) PlayerPrefs.SetInt("coins", 0);

        // Если значение больше нуля, вызываем обновление статистики с анимацией
        UpdateTotalCoins(value > 0 ? false : true);
    }

    /// <summary>Обновление статистики монет (анимация вычитания)</summary>
    public void UpdateTotalCoins(bool blinking)
    {
        texts[1].text = PlayerPrefs.GetInt("coins").ToString();

        // Проигрываем анимацию вычитания
        if (blinking) animator.Play("Subtraction");
    }
}