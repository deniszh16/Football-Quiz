using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [Header("Тексты статистики")]
    [SerializeField] private Text[] texts;

    private void Start()
    {
        UpdateScore();
        UpdateCoins();
    }

    /// <summary>Обновление статистики общего счета</summary>
    public void UpdateScore()
    {
        // Выводим сохраненное значение в статистику
        texts[0].text = PlayerPrefs.GetInt("score").ToString();
    }

    /// <summary>Обновление статистики монет (мигание текста)</summary>
    public void UpdateCoins(bool blinking = false)
    {
        // Если монеты ушли в минус, обнуляем общее количество
        if (PlayerPrefs.GetInt("coins") < 0) PlayerPrefs.SetInt("coins", 0);

        // Выводим сохраненное значение в статистику
        texts[1].text = PlayerPrefs.GetInt("coins").ToString();

        if (blinking)
            // Запускаем анимацию мигания
            StartCoroutine(AnimationText(1.8f, texts[1]));
        else
            // Сбрасываем анимацию
            RecoverText(texts[1]);
    }

    /// <summary>Анимация мигания текста (длительность мигания, текстовый объект)</summary>
    private IEnumerator AnimationText(float seconds, Text text)
    {
        // Перекрашиваем текст в красный цвет
        text.color = Color.red;

        while (seconds > 0)
        {
            yield return new WaitForSeconds(0.15f);
            // Изменяем состояние текста
            text.enabled = !text.enabled;
            // Уменьшаем секунды
            seconds -= 0.15f;
        }

        // Сбрасываем анимацию
        RecoverText(text);
    }

    /// <summary>Восстановление текста после анимации (текстовый объект)</summary>
    private void RecoverText(Text text)
    {
        // Останавливаем мигание
        StopAllCoroutines();

        // Активируем текст
        text.enabled = true;
        // Возвращаем стандартный цвет
        text.color = new Color(0, 0.36078f, 0);
    }
}