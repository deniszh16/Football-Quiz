using UnityEngine;
using UnityEngine.SceneManagement;
using GooglePlayGames;

public class LaunchGame : MonoBehaviour
{
    private void Start()
    {
        #region Saved Data
        // Общий счет викторины
        if (!PlayerPrefs.HasKey("score")) PlayerPrefs.SetInt("score", 0);
        // Общее количество монет
        if (!PlayerPrefs.HasKey("coins")) PlayerPrefs.SetInt("coins", 1000);

        // Прогресс категорий по странам
        if (!PlayerPrefs.HasKey("sets"))
        {
            PlayerPrefs.SetString("sets","{\"ArraySets\": [0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0]}");
            // Количество правильных ответов
            PlayerPrefs.SetInt("countries-answer", 0);
            // Количество допущенных ошибок
            PlayerPrefs.SetInt("countries-error", 0);
            // Количество использованных подсказок
            PlayerPrefs.SetInt("countries-tips", 0);
            // Количество пропусков вопросов
            PlayerPrefs.SetInt("countries-pass", 0);
        }

        // Прогресс викторины по игрокам
        if (!PlayerPrefs.HasKey("players"))
        {
            PlayerPrefs.SetInt("players", 0);
            // Количество допущенных ошибок
            PlayerPrefs.SetInt("players-errors", 0);
            // Количество открытых фрагментов
            PlayerPrefs.SetInt("players-buttons", 0);
            // Количество использованных подсказок
            PlayerPrefs.SetInt("players-tips", 0);

            // Прогресс викторины по тренерам
            PlayerPrefs.SetInt("trainers", 0);
            // Количество допущенных ошибок
            PlayerPrefs.SetInt("trainers-errors", 0);
            // Количество открытых фрагментов
            PlayerPrefs.SetInt("trainers-buttons", 0);
            // Количество использованных подсказок
            PlayerPrefs.SetInt("trainers-tips", 0);
        }

        // Количество пропусков по игрокам
        if (!PlayerPrefs.HasKey("players-pass"))
        {
            PlayerPrefs.SetInt("players-pass", 0);
            PlayerPrefs.SetInt("trainers-pass", 0);
        }

        // Прогресс викторины по фактам
        if (!PlayerPrefs.HasKey("facts"))
        {
            // Количество сыгранных игр
            PlayerPrefs.SetInt("facts", 0);
            // Максимальная серия ответов
            PlayerPrefs.SetInt("facts-series", 0);
            // Количество правильных ответов
            PlayerPrefs.SetInt("facts-answer", 0);
            // Количество допущенных ошибок
            PlayerPrefs.SetInt("facts-errors", 0);
        }

        // Прогресс легендарных карточек
        if (!PlayerPrefs.HasKey("legends"))
        {
            PlayerPrefs.SetString("legends", "{\"Status\": []}");
            // Общее количество открытых карточек
            PlayerPrefs.SetInt("legends-open", 0);
        }

        // Сохранение таблицы лидеров
        if (!PlayerPrefs.HasKey("leaderboard"))
            PlayerPrefs.SetString("leaderboard", "{\"Rating\": 0, \"Names\": [], \"Results\": []}");

        // Сохранение последней даты
        if (!PlayerPrefs.HasKey("date"))
        {
            PlayerPrefs.SetInt("date", 0);
            // Ежедневный рекламный бонус
            PlayerPrefs.SetInt("bonus", 0);
        }
        #endregion

        // Активируем игровые сервисы Google Play
        PlayGamesPlatform.Activate();

        // Переходим в главное меню
        Invoke("GoToMenu", 2.0f);
    }

    /// <summary>Переход в главное меню</summary>
    private void GoToMenu()
    {
        SceneManager.LoadScene(1);
    }
}