using Services.GooglePlay;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Logic.GooglePlay
{
    public class OpeningLeaderboard : MonoBehaviour
    {
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        
        private IGooglePlayService _googlePlayService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService) =>
            _googlePlayService = googlePlayService;

        private void Awake() =>
            _button.onClick.AddListener(OpenLeaderboard);

        private void OpenLeaderboard() =>
            _googlePlayService.ShowLeaderboard();

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OpenLeaderboard);
    }
}