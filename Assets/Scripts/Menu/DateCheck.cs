using System;
using UnityEngine;

public class DateCheck : MonoBehaviour
{
    private void Awake()
    {
        // Если сохраненная дата не равна текущей
        if (PlayerPrefs.GetInt("date") != DateTime.Now.Day)
        {
            // Открываем бонусную рекламу
            PlayerPrefs.SetInt("bonus", 3);

            // Сохраняем обновленную дату
            PlayerPrefs.SetInt("date", DateTime.Now.Day);
        }
    }
}