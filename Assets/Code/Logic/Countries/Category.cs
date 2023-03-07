using System;
using Code.Logic.Helpers;
using Code.Services.PersistentProgress;
using UnityEngine;
using Zenject;

namespace Code.Logic.Countries
{
    public class Category : MonoBehaviour
    {
        [Header("Номер категории")]
        [SerializeField] private int _number;

        public int Number => _number;
        public CategoryAccessibility IsAvailable { get; set; }
        public int CurrentQuestion { get; set; }

        public event Action CategoryPurchased;
        
        public IPersistentProgressService PersistentProgress { get; private set; }

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress) =>
            PersistentProgress = persistentProgress;

        private void Awake()
        {
            IsAvailable = PersistentProgress.UserProgress.CountriesData.Accessibility[Number - ForArrays.MinusOne];
            CurrentQuestion = PersistentProgress.UserProgress.CountriesData.Sets[Number - ForArrays.MinusOne];
        }

        public void ReportCategoryPurchase() =>
            CategoryPurchased?.Invoke();
    }
}