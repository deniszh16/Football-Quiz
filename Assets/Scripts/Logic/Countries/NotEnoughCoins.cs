using Services.PersistentProgress;
using UnityEngine;
using Zenject;
using TMPro;

namespace Logic.Countries
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

        private void FlashingText() =>
            _animator.Play(_flashingText);

        private void ChangeText(string text) =>
            _descriptionText.text = text;

        private void OnDestroy() =>
            _progressService.GetUserProgress.CoinsLacked -= FlashingText;
    }
}