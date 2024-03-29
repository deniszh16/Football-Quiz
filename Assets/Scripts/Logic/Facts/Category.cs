﻿using StaticData.Questions.Facts;
using Logic.Helpers;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Logic.Facts
{
    public class Category : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Facts _facts;
        
        [Header("Категория")]
        [SerializeField] private int _number;
        [SerializeField] private TextMeshProUGUI _statisticsText;
        [SerializeField] private Image _resultIcon;

        [Header("Спрайты результата")]
        [SerializeField] private Sprite _victorySprite;
        [SerializeField] private Sprite _losingSprite;

        public int Number => _number;
        public FactsAccessibility Availability { get; private set; }
        public int CurrentQuestion { get; private set; }
        public int TotalQuestions { get; private set; }

        private void Start()
        {
            GetСategoryInfo();
            CheckCategoryAvailability();
        }

        private void GetСategoryInfo()
        {
            Availability = _facts.ProgressService.GetUserProgress.FactsData.Availability[Number - ForArrays.MinusOne];
            CurrentQuestion = _facts.ProgressService.GetUserProgress.FactsData.Sets[Number - ForArrays.MinusOne];
            TotalQuestions = _facts.StaticDataService.GetFactsCategory(Number).Questions.Count;
        }

        private void CheckCategoryAvailability()
        {
            if (Availability == FactsAccessibility.Available)
            {
                _statisticsText.gameObject.SetActive(true);
                _statisticsText.text = CurrentQuestion + " /" + TotalQuestions;
            }
            else if (Availability == FactsAccessibility.Won)
            {
                _statisticsText.gameObject.SetActive(false);
                _resultIcon.gameObject.SetActive(true);
                _resultIcon.sprite = _victorySprite;
            }
            else if (Availability == FactsAccessibility.Lost)
            {
                _statisticsText.gameObject.SetActive(false);
                _resultIcon.gameObject.SetActive(true);
                _resultIcon.sprite = _losingSprite;
            }
        }
    }
}