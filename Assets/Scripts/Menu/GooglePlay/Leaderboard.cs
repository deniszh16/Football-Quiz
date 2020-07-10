using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SocialPlatforms;
using Cubra.Helpers;

namespace Cubra
{
    public class Leaderboard : MonoBehaviour
    {
        [Header("Рейтинг игрока")]
        [SerializeField] private Text _rating;

        [Header("Таблица лидеров")]
        [SerializeField] private Text _leaders;

        // Анимация загрузки
        private Animator _animator;

        [Header("Компонент скроллинга")]
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Кнопка обновления")]
        [SerializeField] private GameObject _updateButton;

        // Объект для json по таблице лидеров
        private LeaderboardHelper _leaderboardHelper;

        private void Awake()
        {
            _animator = _leaders.gameObject.GetComponent<Animator>();

            _leaderboardHelper = new LeaderboardHelper();
            _leaderboardHelper = JsonUtility.FromJson<LeaderboardHelper>(PlayerPrefs.GetString("leaders"));
        }

        private void Start()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (GooglePlayServices.Authenticated)
                {
                    _leaders.text = "Загрузка...";
                    _animator.Play("Loading");

                    // Отправляем свой результат в таблицу лидеров
                    GooglePlayServices.PostingScoreLeaderboard(PlayerPrefs.GetInt("score"));

                    // Загружаем результаты
                    LoadScoresLeaderboard();
                }
            }
            else
            {
                _updateButton.SetActive(false);
                // Выводим сохраненные данные
                ShowResultsFromFile();
            }
        }

        /// <summary>
        /// Загрузка результатов из удаленной таблицы лидеров
        /// </summary>
        public void LoadScoresLeaderboard()
        {
            //Загружаем десять лучших результатов из публичной таблицы
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard,
                LeaderboardStart.TopScores,
                10,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (data) =>
                {
                    // Записываем позицию текущего игрока
                    _leaderboardHelper.Rating = data.PlayerScore.rank;
                    _rating.text = "Моя позиция - " + data.PlayerScore.rank + " место";

                    // Загружаем информацию по игрокам
                    LoadUsersLeaderboard(data.Scores);
                }
            );
        }

        /// <summary>
        /// Загрузка и отображение информации по игрокам
        /// </summary>
        /// <param name="scores">список результатов</param>
        private void LoadUsersLeaderboard(IScore[] scores)
        {
            // Список id пользователей
            var userIds = new List<string>();
            // Перебираем результаты и добавляем id в список
            foreach (IScore score in scores) userIds.Add(score.userID);

            // Загружаем информацию по пользователям
            Social.LoadUsers(userIds.ToArray(), (users) =>
            {
                // Убираем анимацию загрузки
                _animator.Play("Results");
                _leaders.text = "";

                for (int i = 0; i < scores.Length; i++)
                {
                    // Создаем пользователя и ищем его id массиве
                    IUserProfile user = FindUser(users, scores[i].userID);
                    
                    // Выводим результаты в текстовое поле
                    _leaders.text += (i + 1) + " - " + ((user != null) ? user.userName : "Unknown") + " (" + scores[i].value + ")" + ((i < 9) ? IndentsHelpers.LineBreak(2) : "");
                    
                    // Записываем имена и результаты игроков
                    _leaderboardHelper.Names[i] = (user != null) ? user.userName : "Unknown";
                    _leaderboardHelper.Results[i] = scores[i].value;
                }
                
                // Сохраняем обновленные данные по игрокам
                PlayerPrefs.SetString("leaders", JsonUtility.ToJson(_leaderboardHelper));
            });
            
            // Перемещаем скролл вверх списка
            _scrollRect.verticalNormalizedPosition = 1;
        }

        /// <summary>
        /// Поиск игрока в массиве по id
        /// </summary>
        /// <param name="users">список игроков</param>
        /// <param name="userid">id игрока</param>
        /// <returns>Найденный пользователь</returns>
        private IUserProfile FindUser(IUserProfile[] users, string userid)
        {
            foreach (IUserProfile user in users)
            {
                if (user.id == userid) return user;
            }

            return null;
        }

        /// <summary>
        /// Отображение сохраненных данных по игрокам
        /// </summary>
        private void ShowResultsFromFile()
        {
            // Если рейтинг больше нуля
            if (_leaderboardHelper.Rating > 0)
                // Выводим позицию текущего игрока в рейтинге
                _rating.text = "Моя позиция - " + _leaderboardHelper.Rating.ToString() + " место";
            
            for (int i = 0; i < _leaderboardHelper.Results.Length; i++)
            {
                if (_leaderboardHelper.Results[i] > 0)
                {
                    if (i == 0) _leaders.text = "";

                    // Выводим результаты игроков
                    _leaders.text += (i + 1) + " - " + _leaderboardHelper.Names[i] + " (" + _leaderboardHelper.Results[i] + ")" + ((i < 9) ? IndentsHelpers.LineBreak(2) : "");
                }
            }

            // Перемещаем скролл вверх
            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}