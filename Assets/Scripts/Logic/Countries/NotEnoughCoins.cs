using DZGames.Football.Services;
using UnityEngine;
using VContainer;
using TMPro;

namespace DZGames.Football.Countries
{
    public class NotEnoughCoins : MonoBehaviour
    {
        [Header("Текст описания")]
        [SerializeField] private TextMeshProUGUI _descriptionText;
        [SerializeField] private Animator _animator;
        
        private readonly int _flashingText = Animator.StringToHash("NotEnoughCoins_Animation");
        
        private IPersistentProgressService _progressService;

        [Inject]
        private void Construct(IPersistentProgressService persistentProgress) =>
            _progressService = persistentProgress;

        private void Awake() =>
            _progressService.GetUserProgress.CoinsLacked += FlashingText;
        
        private void OnDestroy() =>
            _progressService.GetUserProgress.CoinsLacked -= FlashingText;

        private void FlashingText() =>
            _animator.Play(_flashingText);

        private void ChangeText(string text) =>
            _descriptionText.text = text;
    }
}