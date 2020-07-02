using UnityEngine;
using Cubra.Helpers;

namespace Cubra
{
    /// <summary>
    /// Увеличение количества элементов (карточек, подборок фактов)
    /// </summary>
    public class IncreaseQuantityItems : MonoBehaviour
    {
        /// <summary>
        /// Добавление недостающих элементов
        /// </summary>
        /// <param name="statuses">объект для записи</param>
        /// <param name="quantity">количество элементов</param>
        /// <param name="key">ключ сохранения</param>
        protected void AddToList(StatusHelper statuses, int quantity, string key)
        {
            // Если добавлены новые элементы
            if (statuses.status.Count < quantity)
            {
                // Подсчитываем разницу
                var difference = quantity - statuses.status.Count;

                // Добавляем недостающие элементы
                for (int i = 0; i < difference; i++)
                    statuses.status.Add("no");

                SaveListStatuses(statuses, key);
            }
        }

        /// <summary>
        /// Сохранение списка статусов
        /// </summary>
        /// <param name="statuses">объект для записи</param>
        /// <param name="key">ключ сохранения</param>
        protected void SaveListStatuses(StatusHelper statuses, string key)
        {
            PlayerPrefs.SetString(key, JsonUtility.ToJson(statuses));
        }
    }
}