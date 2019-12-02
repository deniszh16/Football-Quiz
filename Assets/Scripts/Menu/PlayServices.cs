using UnityEngine;
using GooglePlayGames;

public class PlayServices : MonoBehaviour
{
    private void Start()
    {
        SignGooglePlay();
    }

    /// <summary>Подключение к сервисам Google Play</summary>
    public static void SignGooglePlay()
    {
        // Если доступен интернет, подключаемся к Google Play
        if (Application.internetReachability != NetworkReachability.NotReachable)
            Social.localUser.Authenticate((bool success) => {});
    }

    /// <summary>Просмотр игровых достижений</summary>
    public static void ShowAchievements()
    {
        // Если пользователь авторизирован, отображаем список достижений
        if (Social.localUser.authenticated) Social.ShowAchievementsUI();
        else SignGooglePlay();
    }

    /// <summary>Разблокирование достижения (идентификатор достижения)</summary>
    public static void UnlockingAchievement(string identifier)
    {
        if (Social.localUser.authenticated)
            // Открываем достижение с указанным идентификатором
            Social.ReportProgress(identifier, 100.0f, (bool success) => {});
    }

    /// <summary>Просмотр таблицы лидеров</summary>
    public static void ShowLeaderboard()
    {
        if (Social.localUser.authenticated)
            // Отображаем таблицу лидеров
            PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard);
        else SignGooglePlay();
    }

    /// <summary>Отправка результата в таблицу лидеров (общий счет)</summary>
    public static void PostingScoreLeaderboard(int score)
    {
        if (Social.localUser.authenticated)
            // Отправляем указанный результат в таблицу
            Social.ReportScore(score, GPGSIds.leaderboard, (bool success) => {});
    }
}