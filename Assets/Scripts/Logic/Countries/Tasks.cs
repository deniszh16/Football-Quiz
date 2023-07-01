﻿using System;
using Firebase.Analytics;
using Logic.Helpers;
using Services.Analytics;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.StaticData;
using StaticData.Questions.Countries;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic.Countries
{
    public class Tasks : MonoBehaviour
    {
        [Header("Задание с буквами")]
        [SerializeField] private ArrangementOfLetters _letters;
        [SerializeField] private AnswerFromLetters _answerFromLetters;
        [SerializeField] private DeleteLetter _deleteLetter;
        [SerializeField] private Hints _hints;
        
        [Header("Задание с вариантами")]
        [SerializeField] private ArrangementOfVariants _variants;
        [SerializeField] private AnswerFromVariants _answerFromVariants;

        [Header("Поле вопроса")]
        [SerializeField] private TextMeshProUGUI _textQuestion;

        public CountriesStaticData CountriesStaticData { get; private set; }
        
        public int CurrentCategory => _currentCategory;
        public int CurrentQuestion => _currentQuestion;
        
        private int _currentCategory;
        private int _currentQuestion;

        private const string AnalyticsKey = "countries_category_end";

        private IPersistentProgressService _progressService;
        private IStaticDataService _staticDataService;
        private ISceneLoaderService _sceneLoaderService;
        private ISaveLoadService _saveLoadService;
        private IFirebaseService _firebaseService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService,
            ISceneLoaderService sceneLoader, ISaveLoadService saveLoadService, IFirebaseService firebaseService)
        {
            _progressService = progressService;
            _staticDataService = staticDataService;
            _sceneLoaderService = sceneLoader;
            _saveLoadService = saveLoadService;
            _firebaseService = firebaseService;
        }

        private void Awake()
        {
            GetCurrentTask();
            GetCurrentStaticData();
        }
        
        private void Start() =>
            CheckTaskExists();

        public void GetCurrentTask()
        {
            _currentCategory = ActivePartition.CategoryNumber;
            _currentQuestion = _progressService.UserProgress.CountriesData.Sets[_currentCategory - ForArrays.MinusOne] - 1;
        }

        private void GetCurrentStaticData() =>
            CountriesStaticData = _staticDataService.GetCountriesCategory(_currentCategory);
        
        public void CheckTaskExists()
        {
            if (CurrentQuestion < CountriesStaticData.Questions.Count)
            {
                GetTaskType();
                GetQuestion();
            }
            else
            {
                _firebaseService.SubmitAnEvent(AnalyticsKey, new Parameter("number", CurrentCategory));
                
                _progressService.UserProgress.AddCoins(350);
                _progressService.UserProgress.AddScore(100);
                _saveLoadService.SaveProgress();
                
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
        }

        private void GetTaskType()
        {
            TypeQuestions type = CountriesStaticData.Questions[CurrentQuestion].TypeQuestion;

            switch (type)
            {
                case TypeQuestions.Letters:
                    PrepareTaskWithLetters();
                    break;
                case TypeQuestions.Variant:
                    PrepareTaskWithVariants();
                    break;
                default:
                    throw new Exception("Тип задания не указан!");
            }
        }

        private void PrepareTaskWithLetters()
        {
            _letters.gameObject.SetActive(true);
            _letters.ArrangeLetters();
            _answerFromLetters.enabled = true;
            _answerFromLetters.CustomizeAnswer();
            _deleteLetter.gameObject.SetActive(true);
            _hints.gameObject.SetActive(true);
            _hints.CustomizeButton();
            DisableVariantsSection();
        }

        private void DisableVariantsSection()
        {
            _variants.gameObject.SetActive(false);
            _answerFromVariants.enabled = false;
        }

        private void PrepareTaskWithVariants()
        {
            _variants.gameObject.SetActive(true);
            _variants.ArrangeVariants();
            _answerFromVariants.enabled = true;
            _answerFromVariants.UpdateAnswerField();
            DisableLettersSection();
        }

        private void DisableLettersSection()
        {
            _letters.gameObject.SetActive(false);
            _answerFromLetters.enabled = false;
            _deleteLetter.HideDeleteButton();
            _hints.gameObject.SetActive(false);
        }

        private void GetQuestion() =>
            _textQuestion.text = CountriesStaticData.Questions[_currentQuestion].Task;
    }
}