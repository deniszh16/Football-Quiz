using System;
using Logic.GooglePlay;
using Logic.Helpers;
using UnityEngine;

namespace Logic.Legends
{
    public class Card : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Legends _legends;
        
        [Header("Параметры карточки")]
        [SerializeField] private int _number;
        [SerializeField] private bool _biography;

        [Header("Достижение")]
        [SerializeField] private Achievement _achievement;
        
        public int Number => _number;
        public bool Biography => _biography;
        public bool IsAvailable { get; private set; }

        public event Action CardPurchased;

        private void Start()
        {
            if (CheckCardList() && CheckCardStatus())
                UpdateAvailability();
            
            if (_legends.ProgressService.GetUserProgress.LegendsData.Legends[Number - ForArrays.MinusOne] == LegendStatus.Opened)
                _achievement?.UnlockAchievement();
        }

        private bool CheckCardList() =>
            _legends.ProgressService.GetUserProgress.LegendsData.Legends != null;

        private bool CheckCardStatus() =>
            _legends.ProgressService.GetUserProgress.LegendsData.Legends[Number - ForArrays.MinusOne] ==
            LegendStatus.Opened;

        public void UpdateAvailability()
        {
            IsAvailable = true;
            CardPurchased?.Invoke();
        }
    }
}