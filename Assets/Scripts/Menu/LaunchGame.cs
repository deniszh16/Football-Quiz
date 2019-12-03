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
        
        if (!PlayerPrefs.HasKey("sets"))
        {
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
        }
        
        if (!PlayerPrefs.HasKey("players"))
        {
            // Прогресс викторины по игрокам
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
        
        if (!PlayerPrefs.HasKey("players-pass"))
        {
            // Количество пропусков по игрокам
            PlayerPrefs.SetInt("players-pass", 0);
            // Количество пропусков по тренерам
            PlayerPrefs.SetInt("trainers-pass", 0);
        }
        
        if (!PlayerPrefs.HasKey("facts"))
        {
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
        }
        
        if (!PlayerPrefs.HasKey("legends"))
        {
            // Прогресс легендарных карточек
            PlayerPrefs.SetString("legends", "{\"status\": []}");
            // Общее количество открытых карточек
            PlayerPrefs.SetInt("legends-open", 0);
        }
        
        if (!PlayerPrefs.HasKey("leaders"))
            // Сохранение таблицы лидеров
            PlayerPrefs.SetString("leaders", "{\"Rating\": 0, \"Names\": [], \"Results\": []}");
        
        if (!PlayerPrefs.HasKey("date"))
        {
            // Сохранение последней даты
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