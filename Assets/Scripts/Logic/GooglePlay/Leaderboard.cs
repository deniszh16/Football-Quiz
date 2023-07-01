﻿using System.Collections.Generic;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using Logic.Helpers;
using Services.GooglePlay;
using Services.PersistentProgress;
using Services.SaveLoad;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;
using Zenject;

namespace Logic.GooglePlay
{
    public class Leaderboard : MonoBehaviour
    {
        [Header("Рейтинг игрока")]
        [SerializeField] private TextMeshProUGUI _rating;
        
        [Header("Таблица лидеров")]
        [SerializeField] private TextMeshProUGUI _leaders;
        [SerializeField] private ScrollRect _scrollRect;
        
        [Header("Кнопка обновления")]
        [SerializeField] private GameObject _updateButton;

        private IGooglePlayService _googlePlayService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService, IPersistentProgressService progressService,
            ISaveLoadService saveLoadService)
        {
            _googlePlayService = googlePlayService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Start() =>
            PrepareLeaderboard();

        public void PrepareLeaderboard()
        {
            if (Application.internetReachability != NetworkReachability.NotReachable)
            {
                if (_googlePlayService.Authenticated)
                {
                    _leaders.text = "Загрузка...";
                    
                    SubmitYourResult();
                    LoadScoresLeaderboard();
                }
            }
            else
            {
                _updateButton.SetActive(false);
                ShowSavedResults();
            }
        }

        private void SubmitYourResult() =>
            _googlePlayService.SubmitScoreToLeaderboard(_progressService.UserProgress.Score);

        private void LoadScoresLeaderboard()
        {
            PlayGamesPlatform.Instance.LoadScores(
                GPGSIds.leaderboard,
                LeaderboardStart.TopScores,
                rowCount: 10,
                LeaderboardCollection.Public,
                LeaderboardTimeSpan.AllTime,
                (data) =>
                {
                    _progressService.UserProgress.LeaderboardData.MyRating = data.PlayerScore.rank;

                    string rank = data.PlayerScore.rank > 99 ? ">100" : data.PlayerScore.rank.ToString();
                    _rating.text = "Моя позиция - " + rank + " место";
                    LoadUsersLeaderboard(data.Scores); 
                });
        }

        private void LoadUsersLeaderboard(IScore[] scores)
        {
            List<string> userIds = new List<string>();
            foreach (IScore score in scores)
                userIds.Add(score.userID);

            Social.LoadUsers(userIds.ToArray(), (users) =>
            {
                _leaders.text = "";

                for (int i = 0; i < scores.Length; i++)
                {
                    IUserProfile user = FindUser(users, scores[i].userID);
                    _leaders.text += i + 1;
                    _leaders.text += " - " + (user != null ? user.userName : "Unknown");
                    _leaders.text += " (" + scores[i].value + ")";
                    _leaders.text += i < 9 ? IndentsHelpers.LineBreak(2) : "";

                    _progressService.UserProgress.LeaderboardData.PlayersNames[i] =
                        user != null ? user.userName : "Unknown";
                    _progressService.UserProgress.LeaderboardData.Results[i] = scores[i].value;
                }

                _saveLoadService.SaveProgress();
            });
            
            _scrollRect.verticalNormalizedPosition = 1;
        }
        
        private IUserProfile FindUser(IUserProfile[] users, string userid)
        {
            foreach (IUserProfile user in users)
                if (user.id == userid) return user;

            return null;
        }

        private void ShowSavedResults()
        {
            if (_progressService.UserProgress.LeaderboardData.MyRating > 0)
                _rating.text = "Моя позиция - " + _progressService.UserProgress.LeaderboardData.MyRating + " место";

            for (int i = 0; i < _progressService.UserProgress.LeaderboardData.Results.Length; i++)
            {
                if (_progressService.UserProgress.LeaderboardData.Results[i] > 0)
                {
                    if (i == 0)
                        _leaders.text = "";

                    _leaders.text += i + 1;
                    _leaders.text += " - " + _progressService.UserProgress.LeaderboardData.PlayersNames[i];
                    _leaders.text += " (" + _progressService.UserProgress.LeaderboardData.Results[i] + ")";
                    _leaders.text += i < 9 ? IndentsHelpers.LineBreak(2) : "";
                }
            }
            
            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}