using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    // Событие по окончаию времени таймера
    public UnityEvent losing = new UnityEvent();

    [Header("Элементы таймера")]
    [SerializeField] private GameObject[] dashes;

    // Количество секунд
    private int seconds = 14;

    // Последняя активная черточка
    private int number = 7;

    /// <summary>Отсчет секунд до проигрыша</summary>
    private IEnumerator LevelTimer()
    {
        while (seconds > 0)
        {
            yield return new WaitForSeconds(2);
            // Уменьшаем секунды
            seconds -= 2;

            // Убираем черточку таймера
            dashes[number - 1].SetActive(false);
            // Уменьшаем номер активной черточки
            number--;
        }

        // Вызываем подписанные методы
        losing?.Invoke();
    }

    /// <summary>Восстановление таймера</summary>
    public void ResetTimer()
    {
        // Останавливаем отсчеты
        StopAllCoroutines();

        // Восстанавливаем черточки таймера
        for (int i = 0; i < dashes.Length; i++)
            dashes[i].SetActive(true);

        // Восстанавливаем значения
        seconds = 14;
        number = 7;
    }
}