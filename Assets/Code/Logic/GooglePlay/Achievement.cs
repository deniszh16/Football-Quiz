using Code.Services.GooglePlay;
using UnityEngine;
using Zenject;

namespace Code.Logic.GooglePlay
{
    public class Achievement : MonoBehaviour
    {
        [Header("Id достижения")]
        [SerializeField] private string id;
        
        private IGooglePlayService _googlePlayService;

        [Inject]
        private void Construct(IGooglePlayService googlePlayService) =>
            _googlePlayService = googlePlayService;

        public void UnlockAchievement() =>
            _googlePlayService.UnlockAchievement(id);
    }
}