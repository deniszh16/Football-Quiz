using System;
using PimDeWitte.UnityMainThreadDispatcher;
using AppodealStack.Monetization.Common;
using Services.PersistentProgress;
using Services.SaveLoad;
using Services.Ads;
using UnityEngine.UI;
using UnityEngine;
using Zenject;

namespace Logic.Ads
{
    public class AdsReward : MonoBehaviour
    {
        [Header("Кнопка открытия бонуса")]
        [SerializeField] private Button _button;

        [Header("Бонусная панель")]
        [SerializeField] private GameObject _bonusPanel;
        [SerializeField] private Button _viewAds;
        [SerializeField] private Button _сancel;

        [Header("Кнопки меню")]
        [SerializeField] private GameObject _buttons;

        private IAdService _adService;
        private IPersistentProgressService _progressService;
        private ISaveLoadService _saveLoadService;

        [Inject]
        private void Construct(IPersistentProgressService progressService, ISaveLoadService saveLoadService,
            IAdService adService)
        {
            _adService = adService;
            _progressService = progressService;
            _saveLoadService = saveLoadService;
        }

        private void Awake() =>
            CheckDate();

        private void CheckDate()
        {
            if (_progressService.GetUserProgress.AdsData.DateDay == DateTime.Now.Day) return;
            _progressService.GetUserProgress.AdsData.UpdateDailyBonuses(DateTime.Now.Day);
            _saveLoadService.SaveProgress();
        }

        private void Start()
        {
            CheckNumberOfBonuses();
            
            AppodealCallbacks.RewardedVideo.OnFinished += OnRewardedVideoFinished;
            _button.onClick.AddListener(OpenBonusPanel);
            _viewAds.onClick.AddListener(_adService.ShowRewardedAd);
            _viewAds.onClick.AddListener(HideBonusPanel);
            _сancel.onClick.AddListener(HideBonusPanel);
        }

        private void CheckNumberOfBonuses()
        {
            bool state = _progressService.GetUserProgress.AdsData.NumberOfBonuses > 0;
            _button.gameObject.transform.parent.gameObject.SetActive(state);
        }
        
        private void OnRewardedVideoFinished(object sender, RewardedVideoFinishedEventArgs e)
        {
            UnityMainThreadDispatcher.Instance().Enqueue(()=> {
                AddBonus();
                CheckNumberOfBonuses();
            });
        }

        private void OpenBonusPanel()
        {
            _bonusPanel.SetActive(true);
            _buttons.SetActive(false);
        }

        private void HideBonusPanel()
        {
            _bonusPanel.SetActive(false);
            _buttons.SetActive(true);
        }
        
        private void AddBonus()
        {
            _progressService.GetUserProgress.AddCoins(350);
            _progressService.GetUserProgress.AdsData.NumberOfBonuses -= 1;
            _saveLoadService.SaveProgress();
        }

        private void OnDestroy()
        {
            AppodealCallbacks.RewardedVideo.OnFinished -= OnRewardedVideoFinished;
            _button.onClick.RemoveListener(OpenBonusPanel);
            _viewAds.onClick.RemoveListener(_adService.ShowRewardedAd);
            _viewAds.onClick.RemoveListener(HideBonusPanel);
            _сancel.onClick.RemoveListener(HideBonusPanel);
        }
    }
}