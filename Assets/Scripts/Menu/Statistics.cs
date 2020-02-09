using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [Header("Тексты статистики")]
    [SerializeField] private Text[] texts;

    // Ссылка на анимацию текста
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

    /// <summary>
    /// Изменение общего счета
    /// </summary>
    /// <param name="value">Количество очков</param>
    public void ChangeTotalScore(int value)
    {
        PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") + value);
        UpdateTotalScore();
    }

    /// <summary>
    /// Обновление статистики по общему счету
    /// </summary>
    public void UpdateTotalScore()
    {
        texts[0].text = PlayerPrefs.GetInt("score").ToString();
    }

    /// <summary>
    /// Изменение количества монет
    /// </summary>
    /// <param name="value">Количество монет</param>
    public void ChangeTotalCoins(int value)
    {
        PlayerPrefs.SetInt("coins", PlayerPrefs.GetInt("coins") + value);

        // Если монеты ушли в минус, обнуляем общее количество
        if (PlayerPrefs.GetInt("coins") < 0) PlayerPrefs.SetInt("coins", 0);

        // Если значение меньше нуля, активируем анимацию вычитания
        UpdateTotalCoins(value > 0 ? false : true);
    }

    /// <summary>
    /// Обновление статистики монет
    /// </summary>
    /// <param name="blinking">Анимация вычитания</param>
    public void UpdateTotalCoins(bool blinking)
    {
        texts[1].text = PlayerPrefs.GetInt("coins").ToString();

        // Проигрываем анимацию вычитания
        if (blinking) animator.Play("Subtraction");
    }
}