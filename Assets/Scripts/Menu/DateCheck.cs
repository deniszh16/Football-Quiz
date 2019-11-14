using System;
using UnityEngine;

public class DateCheck : MonoBehaviour
{
    private void Awake()
    {
        // Если сохраненная дата не равна текущей
        if (PlayerPrefs.GetInt("date") != DateTime.Now.Day)
        {
            // Открываем три просмотра видеорекламы
            PlayerPrefs.SetInt("bonus", 3);

            // Сохраняем текущую дату
            PlayerPrefs.SetInt("date", DateTime.Now.Day);
        }
    }
}