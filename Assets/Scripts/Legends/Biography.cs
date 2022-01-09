using UnityEngine;
using UnityEngine.UI;
using Cubra.Helpers;
using TMPro;

namespace Cubra.Legends
{
    public class Biography : FileProcessing
    {
        [Header("Заголовок")]
        [SerializeField] private TextMeshProUGUI _heading;

        [Header("Биография легенды")]
        [SerializeField] private TextMeshProUGUI _biography;

        [Header("Компонент скролла")]
        [SerializeField] private ScrollRect _scrollRect;

        // Объект для json по описанию игрока
        private LegendsHelpers _legendsHelpers;

        private void Awake()
        {
            _legendsHelpers = new LegendsHelpers();
            
            // Обрабатываем json файл и записываем в переменную
            string jsonString = ReadJsonFile("legend-" + Legends.Card);
            // Преобразовываем строку в объект
            ConvertToObject(ref _legendsHelpers, jsonString);
        }

        private void Start()
        {
            _heading.text = _legendsHelpers.Name;
            _biography.text += _legendsHelpers.Progress.Club;

            // Если есть международные достижения
            if (_legendsHelpers.Progress.Team != "")
            {
                _biography.text += IndentsHelpers.LineBreak(2) + IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += _legendsHelpers.Progress.Team;
            }

            // Если есть особые достижения
            if (_legendsHelpers.Progress.Personal != "")
            {
                _biography.text += IndentsHelpers.LineBreak(2) + IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(2);
                _biography.text += _legendsHelpers.Progress.Personal;
                _biography.text += IndentsHelpers.LineBreak(2);
            }

            _scrollRect.verticalNormalizedPosition = 1;
        }
    }
}