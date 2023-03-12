using Code.Data;
using Code.Services.Analytics;
using Code.Services.PersistentProgress;
using Code.Services.SaveLoad;
using UnityEngine;
using Zenject;

namespace Code.Logic.Legends
{
    public class Legends : MonoBehaviour
    {
        [Header("Список карточек")]
        [SerializeField] private GameObject _cards;
        
        private int _numberOfCards;
        private LegendsData _legendsData;
        
        public IPersistentProgressService ProgressService { get; private set; }
        public ISaveLoadService SaveLoadService { get; private set; }
        public IFirebaseService FirebaseService { get; private set; }

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IFirebaseService firebaseService)
        {
            ProgressService = progressService;
            SaveLoadService = saveLoadService;
            FirebaseService = firebaseService;
        }

        private void Awake()
        {
            _numberOfCards = _cards.transform.childCount;
            _legendsData = ProgressService.UserProgress.LegendsData;
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