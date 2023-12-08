using System;
using Logic.Helpers;
using Services.SaveLoad;
using StaticData.Questions.Facts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic.Facts
{
    public class Answer : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Tasks _tasks;
        [SerializeField] private WarningCards _warningCards;
        [SerializeField] private Timer _timer;
        [SerializeField] private UpdateTask _updateTask;
        
        [Header("Поле ответа")]
        [SerializeField] private TextMeshProUGUI _textAnswer;
        
        [Header("Победный эффект")]
        [SerializeField] private ParticleSystem _winningEffect;
        
        public event Action TaskCompleted;

        public bool CurrentAnswer => _currentAnswer;
        private bool _currentAnswer;

        private const string FirstMistake = "Неправильно!\n" + "Получена первая желтая карточка.";
        private const string SecondMistake = "Неправильно!\n" + "Получена красная карточка, подборка провалена.";
        private const string FirstTimerCompleted = "Время закончилось!\n" + "Получено предупреждение за затяжку времени.";
        private const string SecondTimerCompleted = "Время закончилось!\n" + "Получена красная карточка, подборка провалена.";

        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(ISaveLoadService saveLoadService) =>
            _saveLoadService = saveLoadService;

        public void GetAnswer() =>
            _currentAnswer = _tasks.FactsStaticData.Questions[_tasks.CurrentQuestion].Answer;

        public void CheckAnswer(bool selectedAnswer)
        {
            if (_currentAnswer == selectedAnswer)
            {
                ShowWinningEffect();
                _textAnswer.text = _tasks.FactsStaticData.Questions[_tasks.CurrentQuestion].Description;
                UpdateCategoryProgress(isAnswerTrue: true);
            }
            else
            {
                _warningCards.AddCard();
                CheckNumberOfCards();
                UpdateCategoryProgress(isAnswerTrue: false);
            }
            
            TaskCompleted?.Invoke();
            _updateTask.ToggleButton(state: true);
        }

        private void ShowWinningEffect()
        {
            _winningEffect.Stop();
            _winningEffect.Play();
        }

        private void CheckNumberOfCards()
        {
            if (_warningCards.ReceivedCards == 1)
            {
                _textAnswer.text = DetermineFirstError();
            }
            else if (_warningCards.ReceivedCards >= 2)
            {
                _textAnswer.text = DetermineSecondError();
                _tasks.ProgressService.GetUserProgress.FactsData.Availability[
                    _tasks.CurrentCategory - ForArrays.MinusOne] = FactsAccessibility.Lost;
            }
        }

        private string DetermineFirstError()
        {
            if (_timer.TimeIsOver)
                return FirstTimerCompleted;

            return FirstMistake;
        }

        private string DetermineSecondError()
        {
            if (_timer.TimeIsOver)
                return SecondTimerCompleted;
            
            return SecondMistake;
        }

        private void UpdateCategoryProgress(bool isAnswerTrue)
        {
            if (isAnswerTrue)
            {
                _tasks.ProgressService.GetUserProgress.AddCoins(50);
                _tasks.ProgressService.GetUserProgress.AddScore(3);
                _tasks.ProgressService.GetUserProgress.FactsData.RightAnswers += 1;
            }
            else
            {
                _tasks.ProgressService.GetUserProgress.SubtractionCoins(20);
                _tasks.ProgressService.GetUserProgress.FactsData.WrongAnswers += 1;
            }

            _tasks.ProgressService.GetUserProgress.FactsData.Sets[_tasks.CurrentCategory - ForArrays.MinusOne] += 1;
            _tasks.ProgressService.GetUserProgress.FactsData.ReceivedCards
                [_tasks.CurrentCategory - ForArrays.MinusOne] = _warningCards.ReceivedCards;
            
            _saveLoadService.SaveProgress();
        }
    }
}