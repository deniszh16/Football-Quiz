using System;
using DZGames.Football.GooglePlay;
using DZGames.Football.Services;
using DZGames.Football.Helpers;
using UnityEngine;
using VContainer;

namespace DZGames.Football.Countries
{
    public class Category : MonoBehaviour
    {
        public event Action CategoryPurchased;
        
        public int Number => _number;
        public CategoryAccessibility IsAvailable { get; set; }
        public int CurrentQuestion { get; set; }
        
        [Header("Номер категории")]
        [SerializeField] private int _number;

        [Header("Достижение")]
        [SerializeField] private Achievement _achievement;
        
        public IPersistentProgressService ProgressService { get; private set; }
        public IStaticDataService StaticDataService { get; private set; }

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress, IStaticDataService staticDataService)
        {
            ProgressService = persistentProgress;
            StaticDataService = staticDataService;
        }

        private void Awake()
        {
            IsAvailable = ProgressService.GetUserProgress.CountriesData.Accessibility[Number - ForArrays.MinusOne];
            CurrentQuestion = ProgressService.GetUserProgress.CountriesData.Sets[Number - ForArrays.MinusOne];
        }

        private void Start()
        {
            if (CurrentQuestion > StaticDataService.GetCountriesCategory(Number).Questions.Count)
                _achievement.UnlockAchievement();
        }

        public void ReportCategoryPurchase() =>
            CategoryPurchased?.Invoke();
    }
}