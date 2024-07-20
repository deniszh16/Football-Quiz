using System;
using UnityEngine;
using DZGames.Football.StaticData.Countries;
using DZGames.Football.Services;
using DZGames.Football.Helpers;
using VContainer;
using TMPro;

namespace DZGames.Football.Countries
{
    public class Tasks : MonoBehaviour
    {
        public CountriesStaticData CountriesStaticData { get; private set; }
        
        public int CurrentCategory => _currentCategory;
        public int CurrentQuestion => _currentQuestion;
        
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
        
        private int _currentCategory;
        private int _currentQuestion;

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
            _currentQuestion = _progressService.GetUserProgress.CountriesData.Sets[_currentCategory - ForArrays.MinusOne] - 1;
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
                _firebaseService.SubmitAnEvent(AnalyticsKeys.CountriesCategoryEnd, ("number", CurrentCategory));
                _progressService.GetUserProgress.AddCoins(350);
                _progressService.GetUserProgress.AddScore(100);
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