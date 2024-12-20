﻿using DZGames.Football.Helpers;
using DZGames.Football.Services;
using UnityEngine;
using VContainer;
using TMPro;

namespace DZGames.Football.Players
{
    public class Answer : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        [SerializeField] private ArrangementOfVariants _variants;
        [SerializeField] private UpdateTask _updateTask;
        
        [Header("Тексты задания")]
        [SerializeField] private TextMeshProUGUI _textAnswer;
        [SerializeField] private TextMeshProUGUI _textTarget;
        [SerializeField] private TextMeshProUGUI _textAttempts;

        private int _target;
        private int _attempts;

        private const string LeftToFind = "Осталось найти: ";
        private const string AvailableTries = "Доступные попытки: ";

        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        public void GetAnswer()
        {
            _target = _tasks.PlayersStaticData.Questions[_tasks.CurrentQuestion].NumberOfAnswers;
            _attempts = _tasks.PlayersStaticData.Questions[_tasks.CurrentQuestion].Attempts;
            ShowTargetAndAttempts();
        }

        private void ShowTargetAndAttempts()
        {
            _textTarget.text = LeftToFind + _target;
            _textAttempts.text = AvailableTries + _attempts;
        }

        public void CheckAnswer(VariantButton variant)
        {
            _attempts -= 1;

            if (variant.CorrectVariant)
            {
                _target -= 1;
                variant.ShowFrame();
                _tasks.ProgressService.GetUserProgress.PlayersData.RightAnswers += 1;
            }
            else
            {
                variant.ShowFrame();
                _tasks.ProgressService.GetUserProgress.PlayersData.WrongAnswers += 1;
            }

            _saveLoadService.SaveProgress();
            ShowTargetAndAttempts();
            CheckTargetAndAttempts();
        }

        private void CheckTargetAndAttempts()
        {
            if (_target > 0)
            {
                if (_attempts < _target)
                {
                    ShowAnswerDescription();
                    _variants.HideWrongVariants();
                    _updateTask.TaskUpdateButton.SetActive(true);
                    _updateTask.ToggleButton(state: true);
                    UpdateCategoryProgress(taskComplete: false);
                }
            }
            else
            {
                ShowAnswerDescription();
                _variants.HideWrongVariants();
                _updateTask.TaskUpdateButton.SetActive(true);
                _updateTask.ToggleButton(state: true);
                UpdateCategoryProgress(taskComplete: true);
            }
        }

        private void ShowAnswerDescription() =>
            _textAnswer.text = _tasks.PlayersStaticData.Questions[_tasks.CurrentQuestion].Description;

        private void UpdateCategoryProgress(bool taskComplete)
        {
            if (taskComplete)
            {
                _tasks.ProgressService.GetUserProgress.AddCoins(50);
                _tasks.ProgressService.GetUserProgress.AddScore(5);
                _tasks.ProgressService.GetUserProgress.PlayersData.Completed += 1;
            }
            else
            {
                _tasks.ProgressService.GetUserProgress.SubtractionCoins(20);
            }

            _tasks.ProgressService.GetUserProgress.PlayersData.Sets[_tasks.CurrentCategory - ForArrays.MinusOne] += 1;
            _saveLoadService.SaveProgress();
        }
    }
}