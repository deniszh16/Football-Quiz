﻿using DZGames.Football.Services;
using DZGames.Football.Data;
using UnityEngine;
using VContainer;

namespace DZGames.Football.Facts
{
    public class Facts : MonoBehaviour
    {
        public IPersistentProgressService ProgressService { get; private set; }
        public IStaticDataService StaticDataService { get; private set; }
        
        private ISaveLoadService _saveLoadService;
        private FactsData _factsData;

        [Inject]
        private void Construct(IPersistentProgressService progressService, IStaticDataService staticDataService,
            ISaveLoadService saveLoadService)
        {
            ProgressService = progressService;
            StaticDataService = staticDataService;
            _saveLoadService = saveLoadService;
        }

        private void Awake()
        {
            _factsData = ProgressService.GetUserProgress.FactsData;
            FactsInitialization();
        }

        private void FactsInitialization()
        {
            int factsCategories = StaticDataService.GetNumberOfFactsCategories();

            if (_factsData.Sets == null)
            {
                _factsData.InitializeList(factsCategories);
                _saveLoadService.SaveProgress();
            }
            else if (_factsData.Sets.Count < factsCategories)
            {
                int difference = factsCategories - _factsData.Sets.Count;
                _factsData.EnlargeList(difference);
                _saveLoadService.SaveProgress();
            }
        }
    }
}