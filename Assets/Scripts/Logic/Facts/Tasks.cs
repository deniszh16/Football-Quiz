using Logic.Helpers;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.SceneLoader;
using Services.StaticData;
using StaticData.Questions.Facts;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic.Facts
{
    public class Tasks : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Answer _answer;
        [SerializeField] private WarningCards _warningCards;
        [SerializeField] private Variants _variants;
        [SerializeField] private Timer _timer;
        
        [Header("Поле вопроса")]
        [SerializeField] private TextMeshProUGUI _textQuestion;
        
        public int CurrentQuestion => _currentQuestion;
        public int CurrentCategory => _currentCategory;
        
        private int _currentCategory;
        private int _currentQuestion;

        public FactsStaticData FactsStaticData { get; private set; }
        
        public IPersistentProgressService ProgressService { get; private set; }
        private ISaveLoadService _saveLoadService;
        private IStaticDataService _staticDataService;
        private ISceneLoaderService _sceneLoaderService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService,
            ISceneLoaderService sceneLoader, ISaveLoadService saveLoadService)
        {
            ProgressService = progressService;
            _staticDataService = staticDataService;
            _sceneLoaderService = sceneLoader;
            _saveLoadService = saveLoadService;
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
            _currentQuestion = ProgressService.UserProgress.FactsData.Sets[CurrentCategory - ForArrays.MinusOne];
        }

        private void GetCurrentStaticData() =>
            FactsStaticData = _staticDataService.GetFactsCategory(CurrentCategory);

        public void CheckTaskExists()
        {
            if (CurrentQuestion < FactsStaticData.Questions.Count && CheckCategoryAvailability())
            {
                GetQuestion();
                PrepareTaskWithVariants();
            }
            else if (CurrentQuestion >= FactsStaticData.Questions.Count)
            {
                ProgressService.UserProgress.FactsData.Availability[CurrentCategory - ForArrays.MinusOne] = FactsAccessibility.Won;
                ProgressService.UserProgress.AddCoins(350);
                ProgressService.UserProgress.AddScore(100);
                ProgressService.UserProgress.FactsData.Completed += 1;
                ProgressService.UserProgress.FactsData.Victory += 1;
                _saveLoadService.SaveProgress();
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
            else
            {
                ProgressService.UserProgress.FactsData.Completed += 1;
                _saveLoadService.SaveProgress();
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
        }

        private bool CheckCategoryAvailability() =>
            ProgressService.UserProgress.FactsData.Availability[CurrentCategory - ForArrays.MinusOne] ==
            FactsAccessibility.Available;

        private void GetQuestion() =>
            _textQuestion.text = FactsStaticData.Questions[CurrentQuestion].Question;

        private void PrepareTaskWithVariants()
        {
            _answer.GetAnswer();
            _warningCards.GetCurrentCards();
            _variants.ShowButtons();
            _timer.ResetTimer();
            _timer.StartCountdown();
        }
    }
}