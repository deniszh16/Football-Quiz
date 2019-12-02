using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsInMenu : MonoBehaviour
{
    [Header("Активность возврата")]
    [SerializeField] private bool backButton = true;

    [Header("Сцена для возврата")]
    [SerializeField] private int previousScene;

    // Ссылка на страницу игры в Google Play
    private const string link = "https://play.google.com/store/apps/details?id=ru.cubra.football";

    private void Update()
    {
        // Если активен возврат и нажата кнопка возврата
        if (backButton && Input.GetKey(KeyCode.Escape))
            GoToScene(previousScene);
    }

    /// <summary>Переход на указанную сцену (номер сцены)</summary>
    public void GoToScene(int scene)
    {
        SceneManager.LoadScene(scene);
    }

    /// <summary>Перезапуск текущей сцены</summary>
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    /// <summary>Переход на страницу приложения</summary>
    public void LeaveFeedback()
    {
        Application.OpenURL(link);
    }

    /// <summary>Просмотр достижений Google Play</summary>
    public void ViewAchievements()
    {
        PlayServices.ShowAchievements();
    }

    /// <summary>Просмотр таблицы лидеров Google Play</summary>
    public void ViewLeaderboard()
    {
        PlayServices.ShowLeaderboard();
    }

    /// <summary>Выход из игры</summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}