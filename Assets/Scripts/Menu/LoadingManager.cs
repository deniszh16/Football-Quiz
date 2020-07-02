using UnityEngine;
//using GooglePlayGames;
using Cubra.Helpers;

namespace Cubra
{
    public class LoadingManager : MonoBehaviour
    {
        private void Awake()
        {
            #region Saved Data
            if (PlayerPrefs.HasKey("score") == false)
            {
                // Общий счет викторины
                PlayerPrefs.SetInt("score", 0);
                // Общее количество монет
                PlayerPrefs.SetInt("coins", 1000);

                // Прогресс категорий по странам
                PlayerPrefs.SetString("sets", "{\"arraySets\": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]}");
                // Количество правильных ответов
                PlayerPrefs.SetInt("countries-answer", 0);
                // Количество допущенных ошибок
                PlayerPrefs.SetInt("countries-error", 0);
                // Количество использованных подсказок
                PlayerPrefs.SetInt("countries-tips", 0);
                // Количество пропусков вопросов
                PlayerPrefs.SetInt("countries-pass", 0);

                // Прогресс викторины по фактам
                PlayerPrefs.SetString("facts", "{\"status\": []}");
                // Количество завершенных подборок
                PlayerPrefs.SetInt("facts-quantity", 0);
                // Количество выигранных подборок
                PlayerPrefs.SetInt("facts-victory", 0);
                // Количество правильных ответов
                PlayerPrefs.SetInt("facts-answer", 0);
                // Количество допущенных ошибок
                PlayerPrefs.SetInt("facts-errors", 0);

                // Прогресс легендарных карточек
                PlayerPrefs.SetString("legends", "{\"status\": []}");
                // Количество открытых карточек
                PlayerPrefs.SetInt("legends-open", 0);

                // Сохранение таблицы лидеров
                PlayerPrefs.SetString("leaders", "{\"Rating\": 0, \"Names\": [\"User\", \"User\", \"User\", \"User\", \"User\", \"User\", \"User\", \"User\", \"User\", \"User\"], \"Results\": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0]}");

                // Сохранение последней даты
                PlayerPrefs.SetInt("date", 0);
                // Ежедневный рекламный бонус
                PlayerPrefs.SetInt("bonus", 0);
            }
            #endregion

            // Обновление категорий по странам
            if (PlayerPrefs.HasKey("update") == false)
            {
                RefreshCategories();
                PlayerPrefs.SetString("update", "performed");
            }

            // Активируем игровые сервисы Google Play
            //PlayGamesPlatform.Activate();
        }

        private void Start()
        {
            var transitions = Camera.main.GetComponent<TransitionsManager>();
            // Переходим в главное меню
            _ = StartCoroutine(transitions.GoToSceneWithPause(1.5f, 1));
        }

        /// <summary>
        /// Обновление категорий (перестановка для версии 1.3.5)
        /// </summary>
        private void RefreshCategories()
        {
            var sets = JsonUtility.FromJson<SetsHelper>(PlayerPrefs.GetString("sets"));

            // Старый прогресс в категориях
            int[] progress = new int[3];

            for (int i = 0; i < 3; i++)
                progress[i] = sets.arraySets[i + 12];

            // Обнуляем новую категорию
            sets.arraySets[12] = 0;

            for (int i = 0; i < 3; i++)
                // Переставляем прогресс категорий
                sets.arraySets[i + 13] = progress[i];

            // Сохраняем обновленное значение
            PlayerPrefs.SetString("sets", JsonUtility.ToJson(sets));
        }
    }
}