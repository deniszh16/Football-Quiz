using System;
using Code.Logic.Helpers;
using UnityEngine;

namespace Code.Logic.Legends
{
    public class Card : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private Legends _legends;
        
        [Header("Параметры карточки")]
        [SerializeField] private int _number;
        [SerializeField] private bool _biography;
        
        public int Number => _number;
        public bool Biography => _biography;
        public bool IsAvailable { get; private set; }

        public event Action CardPurchased;

        private void Awake()
        {
            if (CheckCardList() && CheckCardStatus())
                IsAvailable = true;
        }

        private bool CheckCardList() =>
            _legends.ProgressService.UserProgress.LegendsData.Legends != null;

        private bool CheckCardStatus() =>
            _legends.ProgressService.UserProgress.LegendsData.Legends[Number - ForArrays.MinusOne] ==
            LegendStatus.Opened;

        public void UpdateAvailability()
        {
            IsAvailable = true;
            CardPurchased?.Invoke();
        }
    }
}