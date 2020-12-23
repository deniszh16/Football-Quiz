using UnityEngine;
using GooglePlayGames;

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

            // Прогресс по фотографиям игроков и тренеров
            if (PlayerPrefs.HasKey("photos-players") == false)
            {
                PlayerPrefs.SetInt("photos-players", 0);
                // Количество успешно завершенных заданий
                PlayerPrefs.SetInt("photos-successfully", 0);
                // Количество правильных ответов
                PlayerPrefs.SetInt("photos-answer", 0);
                // Количество ошибок
                PlayerPrefs.SetInt("photos-errors", 0);
            }

            // Отображение рекламных баннеров
            if (PlayerPrefs.HasKey("show-ads") == false)
            {
                PlayerPrefs.SetString("show-ads", "yes");
            }

            // Активируем игровые сервисы Google Play
            PlayGamesPlatform.Activate();
        }

        private void Start()
        {
            var transitions = Camera.main.GetComponent<TransitionsManager>();
            _ = StartCoroutine(transitions.GoToSceneWithPause(1.8f, (int)TransitionsManager.Scenes.Menu));
        }
    }
}