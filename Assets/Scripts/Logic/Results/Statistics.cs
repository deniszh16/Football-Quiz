﻿using Data;
using Logic.Helpers;
using Services.PersistentProgress;
using Services.StaticData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.Results
{
    public class Statistics : MonoBehaviour
    {
        [Header("СТекст статистики")]
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private TextMeshProUGUI _results;
        
        [Header("Кубки за прогресс")]
        [SerializeField] private int[] _progress;
        [SerializeField] private Image[] _cups;
        [SerializeField] private TextMeshProUGUI _textPercent;

        private int _percentages;
        private int _overallProgress;

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
        }

        private void Start()
        {
            GetFullStatistics();
            
            _percentages = 0;
            GetOverallProgress();
            GetCups();
            SetPercentageText();
        }

        private void GetFullStatistics()
        {
            int numberOfTasksInCountries = _staticDataService.GetNumberOfTasksInCountries();
            CountriesData countriesData = _progressService.UserProgress.CountriesData;
            
            _results.text = "Викторина по странам" + IndentsHelpers.LineBreak(2);
            _results.text += "Всего вопросов: " + numberOfTasksInCountries + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + countriesData.RightAnswers + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + countriesData.WrongAnswers + IndentsHelpers.LineBreak(1);
            _results.text += "Количество подсказок: " + countriesData.Hints + IndentsHelpers.LineBreak(1);
            _results.text += "Пропуски вопросов: " + countriesData.Pass + IndentsHelpers.LineBreak(1);
            _results.text += "Заработанные очки: " + countriesData.RightAnswers * 5 + IndentsHelpers.LineBreak(2);
            
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);

            int numberOfTasksInFacts = _staticDataService.GetNumberOfTasksInFacts();
            int numberOfCategories = _staticDataService.GetNumberOfFactsCategories();
            FactsData factsData = _progressService.UserProgress.FactsData;
            
            _results.text += "Викторина по фактам" + IndentsHelpers.LineBreak(2);
            _results.text += "Количество подборок: " + numberOfCategories + IndentsHelpers.LineBreak(1);
            _results.text += "Всего вопросов: " + numberOfTasksInFacts + IndentsHelpers.LineBreak(1);
            _results.text += "Завершенные подборки: " + factsData.Completed + IndentsHelpers.LineBreak(1);
            _results.text += "Выигранные подборки: " + factsData.Victory + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + factsData.RightAnswers + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + factsData.WrongAnswers + IndentsHelpers.LineBreak(1);
            _results.text += "Заработанные очки: " + factsData.RightAnswers * 3 + IndentsHelpers.LineBreak(2);
            
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);

            int numberOfTasksInPlayers = _staticDataService.GetNumberOfTasksInPlayers();
            PlayersData playersData = _progressService.UserProgress.PlayersData;
            
            _results.text += "Викторина по фотографиям" + IndentsHelpers.LineBreak(2);
            _results.text += "Количество заданий: " + numberOfTasksInPlayers + IndentsHelpers.LineBreak(1);
            _results.text += "Успешные задания: " + playersData.Completed + IndentsHelpers.LineBreak(1);
            _results.text += "Правильные ответы: " + playersData.RightAnswers + IndentsHelpers.LineBreak(1);
            _results.text += "Количество ошибок: " + playersData.WrongAnswers + IndentsHelpers.LineBreak(2);
            
            _results.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
            
            LegendsData legendsData = _progressService.UserProgress.LegendsData;
            
            _results.text += "Футбольные легенды" + IndentsHelpers.LineBreak(2);
            _results.text += "Всего карточек: 47" + IndentsHelpers.LineBreak(1);
            _results.text += "Открытые карточки: " + legendsData.ReceivedCards + IndentsHelpers.LineBreak(1);
            _results.text += "Потрачено монет: " + legendsData.ReceivedCards * 950 + IndentsHelpers.LineBreak(1);
            
            _scrollRect.verticalNormalizedPosition = 1;
        }

        private void GetOverallProgress()
        {
            int countries = _progressService.UserProgress.CountriesData.RightAnswers;
            int facts = _progressService.UserProgress.FactsData.RightAnswers;
            int players = _progressService.UserProgress.PlayersData.RightAnswers;
            _overallProgress = countries + facts + players;
        }

        private void GetCups()
        {
            for (int i = 0; i < _cups.Length; i++)
            {
                if (_progress[i] <= _overallProgress)
                {
                    _cups[i].color = Color.white;
                    _percentages += 15;
                }
            }
        }

        private void SetPercentageText() =>
            _textPercent.text = "Выигранные кубки (" + (_percentages > 100 ? "100" : _percentages.ToString()) + "%)";
    }
}