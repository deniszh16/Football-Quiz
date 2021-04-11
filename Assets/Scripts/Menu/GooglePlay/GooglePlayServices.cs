using UnityEngine;
using GooglePlayGames;

namespace Cubra
{
    public class GooglePlayServices : MonoBehaviour
    {
        // Аутентификация игрока
        public static bool Authenticated => Social.localUser.authenticated;

        [Header("Автоматическое подключение")]
        [SerializeField] private bool _autoConnect;

        private void Start()
        {
            if (_autoConnect) SignGooglePlay();
        }

        /// <summary>
        /// Подключение к сервисам Google Play
        /// </summary>
        public static void SignGooglePlay()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                Social.localUser.Authenticate((bool success) => {});
            }  
        }

        /// <summary>
        /// Просмотр игровых достижений
        /// </summary>
        public void ShowAchievements()
        {
            if (Authenticated)
            {
                Social.ShowAchievementsUI();
            }
            else
            {
                SignGooglePlay();
            }
        }

        /// <summary>
        /// Разблокирование достижения
        /// </summary>
        /// <param name="identifier">идентификатор достижения</param>
        public static void UnlockingAchievement(string identifier)
        {
            if (Authenticated)
                Social.ReportProgress(identifier, 100.0f, (bool success) => {});
        }

        /// <summary>
        /// Просмотр таблицы лидеров
        /// </summary>
        public void ShowLeaderboard()
        {
            if (Authenticated)
            {
                PlayGamesPlatform.Instance.ShowLeaderboardUI(GPGSIds.leaderboard);
            }
            else
            {
                SignGooglePlay();
            }
        }

        /// <summary>
        /// Отправка результата в таблицу лидеров
        /// </summary>
        /// <param name="score">счет игрока</param>
        public static void PostingScoreLeaderboard(int score)
        {
            if (Authenticated)
                Social.ReportScore(score, GPGSIds.leaderboard, (bool success) => {}); 
        }
    }
}