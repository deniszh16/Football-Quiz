using System;
using Code.Logic.Helpers;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using UnityEngine;
using Zenject;
using TMPro;

namespace Code.Logic.Countries
{
    public abstract class Answer : MonoBehaviour
    {
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
        
        public event Action TaskCompleted;

        private static readonly int InitialAnimation = Animator.StringToHash("Initial");
        protected static readonly int FlashingAnimation = Animator.StringToHash("Flashing");

        protected IPersistentProgressService _progressService;
        protected ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            _progressService = progressService;
            _saveLoadService = saveLoadService;
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
                _progressService.UserProgress.AddCoins(50);
                _progressService.UserProgress.AddScore(5);
                _progressService.UserProgress.CountriesData.RightAnswers += 1;
            }

            _progressService.UserProgress.CountriesData.Sets[_tasks.CurrentCategory - ForArrays.MinusOne] += 1;
            _saveLoadService.SaveProgress();
        }
    }
}