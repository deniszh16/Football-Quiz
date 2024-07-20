using DZGames.Football.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Football.GooglePlay
{
    public class Achievement : MonoBehaviour
    {
        [Header("Идентификатор достижения")]
        [SerializeField] private string id;
        
        private IGooglePlayService _googlePlayService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService) =>
            _googlePlayService = googlePlayService;

        public void UnlockAchievement() =>
            _googlePlayService.UnlockAchievement(id);
    }
}