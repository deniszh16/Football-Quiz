using Services.PersistentProgress;
using StaticData.Questions.Facts;
using Services.SceneLoader;
using Services.StaticData;
using Services.SaveLoad;
using Logic.Helpers;
using UnityEngine;
using Zenject;
using TMPro;

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
            _currentQuestion = ProgressService.GetUserProgress.FactsData.Sets[CurrentCategory - ForArrays.MinusOne];
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
                ProgressService.GetUserProgress.FactsData.Availability[CurrentCategory - ForArrays.MinusOne] = FactsAccessibility.Won;
                ProgressService.GetUserProgress.AddCoins(350);
                ProgressService.GetUserProgress.AddScore(100);
                ProgressService.GetUserProgress.FactsData.Completed += 1;
                ProgressService.GetUserProgress.FactsData.Victory += 1;
                _saveLoadService.SaveProgress();
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
            else
            {
                ProgressService.GetUserProgress.FactsData.Completed += 1;
                _saveLoadService.SaveProgress();
                _sceneLoaderService.Load(Scenes.Results.ToString());
            }
        }

        private bool CheckCategoryAvailability() =>
            ProgressService.GetUserProgress.FactsData.Availability[CurrentCategory - ForArrays.MinusOne] ==
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