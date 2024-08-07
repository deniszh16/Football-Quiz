﻿using DZGames.Football.Services;
using DZGames.Football.Helpers;
using UnityEngine;
using VContainer;
using TMPro;

namespace DZGames.Football.Countries
{
    public class CategoryCurrentResult : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Category _currentСategory;
        
        [Header("Текст результата")]
        [SerializeField] private TextMeshProUGUI _text;

        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticData) =>
            _staticDataService = staticData;

        private void Awake() =>
            _currentСategory.CategoryPurchased += SetValues;

        private void Start() =>
            SetValues();
        
        private void OnDestroy() =>
            _currentСategory.CategoryPurchased -= SetValues;

        private void SetValues()
        {
            if (_currentСategory.IsAvailable == CategoryAccessibility.Available)
            {
                _text.gameObject.SetActive(true);
                _text.text = GetCurrentResult() + " /" + GetNumberOfQuestions();
            }
        }

        private string GetCurrentResult() =>
            (_currentСategory.ProgressService.GetUserProgress.CountriesData
                .Sets[_currentСategory.Number - ForArrays.MinusOne] - 1).ToString();

        private string GetNumberOfQuestions() =>
            _staticDataService.GetCountriesCategory(_currentСategory.Number).Questions.Count.ToString();
    }
}