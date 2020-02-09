using UnityEngine;

public class IncreaseListStatuses : MonoBehaviour
{
    /// <summary>
    /// Добавление недостающих элементов (увеличение списка категорий)
    /// </summary>
    /// <param name="statuses">Объект для записи</param>
    /// <param name="quantity">Количество элементов</param>
    /// <param name="key">Ключ сохранения</param>
    protected void AddToList(StaJson statuses, int quantity, string key)
    {
        // Если добавлены новые элементы
        if (statuses.status.Count < quantity)
        {
            // Подсчитываем разницу
            var difference = quantity - statuses.status.Count;

            // Добавляем недостающие элементы
            for (int i = 0; i < difference; i++)
                statuses.status.Add("no");

            // Сохраняем обновленный список
            SaveListStatuses(statuses, key);
        }
    }

    /// <summary>
    /// Сохранение списка статусов
    /// </summary>
    /// <param name="statuses">Объект для записи</param>
    /// <param name="key">Ключ сохранения</param>
    protected void SaveListStatuses(StaJson statuses, string key)
    {
        PlayerPrefs.SetString(key, JsonUtility.ToJson(statuses));
    }
}