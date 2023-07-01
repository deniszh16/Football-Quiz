using Services.PersistentProgress;
using TMPro;
using UnityEngine;
using Zenject;

namespace Logic.UI
{
    public class TopPanel : MonoBehaviour
    {
        [Header("Тексты валют")]
        [SerializeField] private TextMeshProUGUI _textScore;
        [SerializeField] private TextMeshProUGUI _textCoins;
        
        [Header("Анимация текста")]
        [SerializeField] private Animator _animationTextCoins;
        
        private readonly int _flashingText = Animator.StringToHash("FlashingCoins_Animation");

        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService progressService) =>
            _progressService = progressService;

        private void Awake()
        {
            _progressService.UserProgress.ScoreChanged += UpdateScoreCounter;
            _progressService.UserProgress.CoinsChanged += UpdateCoinsCounter;
            _progressService.UserProgress.CoinsSubstracted += FlashingCoins;
            _progressService.UserProgress.CoinsLacked += FlashingCoins;
        }

        private void Start()
        {
            UpdateScoreCounter();
            UpdateCoinsCounter();
        }

        private void UpdateScoreCounter() =>
            _textScore.text = _progressService.UserProgress.Score.ToString();

        private void UpdateCoinsCounter() =>
            _textCoins.text = _progressService.UserProgress.Coins.ToString();

        private void FlashingCoins() =>
            _animationTextCoins.Play(_flashingText);

        private void OnDestroy()
        {
            _progressService.UserProgress.ScoreChanged -= UpdateScoreCounter;
            _progressService.UserProgress.CoinsChanged -= UpdateCoinsCounter;
            _progressService.UserProgress.CoinsSubstracted -= FlashingCoins;
            _progressService.UserProgress.CoinsLacked -= FlashingCoins;
        }
    }
}