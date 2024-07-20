using DZGames.Football.StaticData.Legends;
using DZGames.Football.Services;
using DZGames.Football.Helpers;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using TMPro;

namespace DZGames.Football.Legends
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
            _biography.text = "";

            foreach (string item in _legendsStaticData.Legend.LegendProgress.Club)
            {
                _biography.text += item;
                _biography.text += IndentsHelpers.LineBreak(2);
            }

            foreach (string item in _legendsStaticData.Legend.LegendProgress.NationalTeam)
            {
                _biography.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += item;
                _biography.text += IndentsHelpers.LineBreak(2);
            }

            foreach (string item in _legendsStaticData.Legend.LegendProgress.PersonalAchievements)
            {
                _biography.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += item;
                _biography.text += IndentsHelpers.LineBreak(2);
            }
            
            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}