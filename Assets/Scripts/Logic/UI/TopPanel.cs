using DZGames.Football.Services;
using UnityEngine;
using VContainer;
using TMPro;

namespace DZGames.Football.UI
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
            _progressService.GetUserProgress.ScoreChanged += UpdateScoreCounter;
            _progressService.GetUserProgress.CoinsChanged += UpdateCoinsCounter;
            _progressService.GetUserProgress.CoinsSubstracted += FlashingCoins;
            _progressService.GetUserProgress.CoinsLacked += FlashingCoins;
        }

        private void Start()
        {
            UpdateScoreCounter();
            UpdateCoinsCounter();
        }

        private void OnDestroy()
        {
            _progressService.GetUserProgress.ScoreChanged -= UpdateScoreCounter;
            _progressService.GetUserProgress.CoinsChanged -= UpdateCoinsCounter;
            _progressService.GetUserProgress.CoinsSubstracted -= FlashingCoins;
            _progressService.GetUserProgress.CoinsLacked -= FlashingCoins;
        }

        private void UpdateScoreCounter() =>
            _textScore.text = _progressService.GetUserProgress.Score.ToString();

        private void UpdateCoinsCounter() =>
            _textCoins.text = _progressService.GetUserProgress.Coins.ToString();

        private void FlashingCoins() =>
            _animationTextCoins.Play(_flashingText);
    }
}