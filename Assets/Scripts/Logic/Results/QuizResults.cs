using DZGames.Football.StaticData.Countries;
using DZGames.Football.StaticData.Players;
using DZGames.Football.StaticData.Facts;
using DZGames.Football.Services;
using DZGames.Football.Helpers;
using DZGames.Football.UI;
using UnityEngine;
using UnityEngine.UI;
using VContainer;
using TMPro;

namespace DZGames.Football.Results
{
    public class QuizResults : MonoBehaviour
    {
        [Header("Тексты результатов")]
        [SerializeField] private TextMeshProUGUI _title;
        [SerializeField] private TextMeshProUGUI _list;
        [SerializeField] private ScrollRect _scrollRect;

        [Header("Кнопка возврата")]
        [SerializeField] private SceneOpenButton _sceneOpenButton;

        private const string TitleCountries = "Список заданий";
        private const string TitleFacts = "Список ф   актов";
        private const string TitlePlayers = "Список заданий";

        private const string Answer = "Ответ: ";
        private const string TextTrue = "Правда";
        private const string TextNotTrue = "Неправда";

        private IStaticDataService _staticDataService;

        [Inject]
        private void Construct(IStaticDataService staticDataService) =>
            _staticDataService = staticDataService;

        private void Start() =>
            GetActivePartition();

        private void GetActivePartition()
        {
            if (ActivePartition.SectionsGame == SectionsGame.Countries)
            {
                _title.text = TitleCountries;
                CountriesStaticData staticData = _staticDataService.GetCountriesCategory(ActivePartition.CategoryNumber);
                
                foreach (var question in staticData.Questions)
                {
                    _list.text += IndentsHelpers.LineBreak(1) + question.Task + IndentsHelpers.LineBreak(2);
                    _list.text += Answer + question.FullAnswer + IndentsHelpers.LineBreak(1);
                    _list.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
                }
                
                CustomizeScroll();
                _sceneOpenButton.ReplaceScene(Scenes.CountriesSets);
            }
            else if (ActivePartition.SectionsGame == SectionsGame.Facts)
            {
                _title.text = TitleFacts;
                FactsStaticData staticData = _staticDataService.GetFactsCategory(ActivePartition.CategoryNumber);

                foreach (Fact question in staticData.Questions)
                {
                    _list.text += IndentsHelpers.LineBreak(1) + question.Question + IndentsHelpers.LineBreak(2);
                    _list.text += Answer;

                    if (question.Answer)
                        _list.text += TextTrue + IndentsHelpers.LineBreak(1);
                    else
                        _list.text += TextNotTrue + IndentsHelpers.LineBreak(1);
                    
                    _list.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
                }
                
                CustomizeScroll();
                _sceneOpenButton.ReplaceScene(Scenes.FactsSets);
            }
            else if (ActivePartition.SectionsGame == SectionsGame.Players)
            {
                _title.text = TitlePlayers;
                PlayersStaticData staticData = _staticDataService.GetPlayersCategory(ActivePartition.CategoryNumber);

                foreach (var question in staticData.Questions)
                {
                    _list.text += IndentsHelpers.LineBreak(1) + question.Task + IndentsHelpers.LineBreak(2);
                    _list.text += Answer + question.Description + IndentsHelpers.LineBreak(1);
                    _list.text += IndentsHelpers.Underscore(26) + IndentsHelpers.LineBreak(1);
                }
                
                CustomizeScroll();
                _sceneOpenButton.ReplaceScene(Scenes.PlayersSets);
            }
        }
        
        private void CustomizeScroll() =>
            _scrollRect.verticalNormalizedPosition = 1;
    }
}