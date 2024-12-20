﻿using DZGames.Football.Data;
using DZGames.Football.Services;
using UnityEngine;
using VContainer;

namespace DZGames.Football.Legends
{
    public class Legends : MonoBehaviour
    {
        [Header("Список карточек")]
        [SerializeField] private GameObject _cards;
        
        private int _numberOfCards;
        private LegendsData _legendsData;
        
        public IPersistentProgressService ProgressService { get; private set; }
        public ISaveLoadService SaveLoadService { get; private set; }

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService)
        {
            ProgressService = progressService;
            SaveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _numberOfCards = _cards.transform.childCount;
            _legendsData = ProgressService.GetUserProgress.LegendsData;
            LegendsInitialization();
        }

        private void LegendsInitialization()
        {
            if (_legendsData.Legends == null)
            {
                _legendsData.InitializeList(_numberOfCards);
                SaveLoadService.SaveProgress();
            }
            else if (_legendsData.Legends.Count < _numberOfCards)
            {
                int difference = _numberOfCards - _legendsData.Legends.Count;
                _legendsData.EnlargeList(difference);
                SaveLoadService.SaveProgress();
            }
        }
    }
}