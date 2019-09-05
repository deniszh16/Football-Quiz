using UnityEngine;
using UnityEngine.SceneManagement;

public class TransitionsInMenu : MonoBehaviour
{
    [Header("Активность возврата")]
    [SerializeField] protected bool backButton = true;

    [Header("Сцена для возврата")]
    [SerializeField] protected int previousScene;

    protected void Update()
    {
        // При нажатии на кнопку возврата, выполняется переход на указанную сцену
        if (backButton && Input.GetKey(KeyCode.Escape)) SceneManager.LoadScene(previousScene);
    }

    // Переход на указанную сцену
    public void GoToScene(int scene) { SceneManager.LoadScene(scene); }

    // Перезапуск сцены
    public void RestartScene() { SceneManager.LoadScene(SceneManager.GetActiveScene().name); }

    // Переход на страницу приложения
    public void LeaveFeedback() { Application.OpenURL("https://play.google.com/store/apps/details?id=ru.cubra.football"); }

    // Просмотр достижений Google Play
    public void ViewAchievements() { PlayServices.ShowAchievements(); }

    // Просмотр таблицы лидеров Google Play
    public void ViewLeaderboard() { PlayServices.ShowLeaderboard(); }

    // Выход из игры
    public void ExitGame() { Application.Quit(); }
}