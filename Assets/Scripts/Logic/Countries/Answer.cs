using System;
using DZGames.Football.Helpers;
using DZGames.Football.Services;
using UnityEngine;
using VContainer;
using TMPro;

namespace DZGames.Football.Countries
{
    public abstract class Answer : MonoBehaviour
    {
        public event Action TaskCompleted;
        
        private static readonly int InitialAnimation = Animator.StringToHash("Initial");
        protected static readonly int FlashingAnimation = Animator.StringToHash("Flashing");
        
        [Header("Ссылки на компоненты")]
        [SerializeField] protected Tasks _tasks;
        [SerializeField] protected UpdateTask _updateTask;
        
        [Header("Текст вопроса")]
        [SerializeField] protected TextMeshProUGUI _textQuestion;
        
        [Header("Текст ответа")]
        [SerializeField] protected TextMeshProUGUI _textAnswer;
        [SerializeField] protected Animator _textAnswerAnimator;

        [Header("Победный эффект")]
        [SerializeField] private ParticleSystem _winningEffect;

        protected IPersistentProgressService _progressService;
        protected ISaveLoadService _saveLoadService;
        protected IAdService _adService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService, IAdService adService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
            _adService = adService;
        }

        protected void ReportCorrectAnswer() =>
            TaskCompleted?.Invoke();
        
        protected void ShowWinningEffect()
        {
            _winningEffect.Stop();
            _winningEffect.Play();
        }

        public void ResetTextAnimation() =>
            _textAnswerAnimator.Play(InitialAnimation);
        
        protected void UpdateCategoryProgress(bool isSkipped)
        {
            if (isSkipped == false)
            {
                _progressService.GetUserProgress.AddCoins(50);
                _progressService.GetUserProgress.AddScore(5);
                _progressService.GetUserProgress.CountriesData.RightAnswers += 1;
            }

            _progressService.GetUserProgress.CountriesData.Sets[_tasks.CurrentCategory - ForArrays.MinusOne] += 1;
            _saveLoadService.SaveProgress();
        }
    }
}