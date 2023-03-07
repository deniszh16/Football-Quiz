using System;
using Code.Logic.Helpers;
using Code.Services.StaticData;
using Code.StaticData.Legends;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using TMPro;

namespace Code.Logic.Legends
{
    public class Biography : MonoBehaviour
    {
        [Header("Тексты описания")]
        [SerializeField] private TextMeshProUGUI _heading;
        [SerializeField] private TextMeshProUGUI _biography;
        [SerializeField] private ScrollRect _scrollRect;

        private IStaticDataService _staticDataService;
        private LegendsStaticData _legendsStaticData;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Awake() =>
            _legendsStaticData = _staticDataService.GetLegends(ActivePartition.CategoryNumber);

        private void Start()
        {
            _heading.text = _legendsStaticData.Legend.Name;
            _biography.text += _legendsStaticData.Legend.LegendProgress.Club;

            if (String.IsNullOrEmpty(_legendsStaticData.Legend.LegendProgress.NationalTeam) == false)
            {
                _biography.text += IndentsHelpers.LineBreak(2) + IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += _legendsStaticData.Legend.LegendProgress.NationalTeam;
            }

            if (String.IsNullOrEmpty(_legendsStaticData.Legend.LegendProgress.PersonalAchievements) == false)
            {
                _biography.text += IndentsHelpers.LineBreak(2) + IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += _legendsStaticData.Legend.LegendProgress.PersonalAchievements;
                _biography.text += IndentsHelpers.LineBreak(2);
            }
            
            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}