using DZGames.Football.Services;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace DZGames.Football.GooglePlay
{
    public class AchievementWindow : MonoBehaviour
    {
        [Header("Кнопка открытия")]
        [SerializeField] private Button _button;
        
        private IGooglePlayService _googlePlayService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService) =>
            _googlePlayService = googlePlayService;

        private void Awake() =>
            _button.onClick.AddListener(OpenAchievementsWindow);

        private void OnDestroy() =>
            _button.onClick.RemoveListener(OpenAchievementsWindow);

        private void OpenAchievementsWindow() =>
            _googlePlayService.ShowAchievements();
    }
}