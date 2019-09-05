using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Statistics : MonoBehaviour
{
    [Header("Тексты статистики")]
    [SerializeField] private Text[] texts;

    // Стандартный цвет текста статистики
    private Color standart = new Color(0, 0.36078f, 0);

    private void Start() { UpdateScore(); UpdateCoins(); }

    // Обновление статистики общего счета
    public void UpdateScore()
    {
        // Выводим сохраненное значение в статистику
        texts[0].text = PlayerPrefs.GetInt("score").ToString();
    }

    // Обновление статистики монет
    public void UpdateCoins(bool animation = false)
    {
        // Если монеты ушли в минус, обнуляем количество
        if (PlayerPrefs.GetInt("coins") < 0) PlayerPrefs.SetInt("coins", 0);

        // Выводим сохраненное значение в статистику
        texts[1].text = PlayerPrefs.GetInt("coins").ToString();
        // Если активирована анимация, запускаем мигание
        if (animation) StartCoroutine(AnimationText(1.8f, texts[1]));
        // Иначе восстанавливаем текст
        else RecoverText();
    }

    // Анимация мигания текста
    private IEnumerator AnimationText(float seconds, Text text)
    {
        // Устанавливаем красный цвет текста
        text.color = Color.red;

        while (seconds > 0)
        {
            // Отсчитываем указанное время
            yield return new WaitForSeconds(0.15f);
            // Изменяем состояние текста
            text.enabled = !text.enabled;
            // Уменьшаем количество секунд
            seconds -= 0.15f;
        }

        // Восстанавливаем тексты
        RecoverText();
    }

    private void RecoverText()
    {
        // Останавливаем все анимации
        StopAllCoroutines();

        for (int i = 0; i < texts.Length; i++)
        {
            // Активируем текст
            texts[i].enabled = true;
            // Возвращаем стандартный цвет
            texts[i].color = standart;
        }
    }
}