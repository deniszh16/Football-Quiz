using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using StaticData.Legends;
using StaticData.Questions.Countries;
using StaticData.Questions.Facts;
using StaticData.Questions.Players;

namespace Services.StaticData
{
    public class StaticDataService : IStaticDataService
    {
        private const string PathQuestionsCountries = "StaticData/Countries";
        private const string PathQuestionsFacts = "StaticData/Facts";
        private const string PathQuestionsPlayers = "StaticData/Players";
        private const string PathLegends = "StaticData/Legends";

        private Dictionary<int, CountriesStaticData> _countries;
        private Dictionary<int, FactsStaticData> _facts;
        private Dictionary<int, PlayersStaticData> _players;
        private Dictionary<int, LegendsStaticData> _legends;

        public void LoadQuestionsCountries()
        {
            _countries = Resources.LoadAll<CountriesStaticData>(PathQuestionsCountries)
                .ToDictionary(x => x.CategoryNumber, x => x);
        }

        public CountriesStaticData GetCountriesCategory(int categoryNumber) =>
            _countries.TryGetValue(categoryNumber, out CountriesStaticData staticData)
                ? staticData
                : null;

        public int GetNumberOfTasksInCountries()
        {
            int numberOfTasks = 0;
            
            foreach (CountriesStaticData item in _countries.Values)
                numberOfTasks += item.Questions.Count;

            return numberOfTasks;
        }

        public void LoadQuestionsFacts()
        {
            _facts = Resources.LoadAll<FactsStaticData>(PathQuestionsFacts)
                .ToDictionary(x => x.CategoryNumber, x => x);
        }
        
        public FactsStaticData GetFactsCategory(int categoryNumber) =>
            _facts.TryGetValue(categoryNumber, out FactsStaticData staticData)
                ? staticData
                : null;

        public int GetNumberOfFactsCategories() =>
            _facts.Count;

        public int GetNumberOfTasksInFacts()
        {
            int numberOfTasks = 0;
            
            foreach (FactsStaticData item in _facts.Values)
                numberOfTasks += item.Questions.Count;
            
            return numberOfTasks;
        }

        public void LoadQuestionsPlayers()
        {
            _players = Resources.LoadAll<PlayersStaticData>(PathQuestionsPlayers)
                .ToDictionary(x => x.CategoryNumber, x => x);
        }

        public PlayersStaticData GetPlayersCategory(int categoryNumber) =>
            _players.TryGetValue(categoryNumber, out PlayersStaticData staticData)
                ? staticData
                : null;

        public int GetNumberOfTasksInPlayers()
        {
            int numberOfTasks = 0;
            
            foreach (PlayersStaticData item in _players.Values)
                numberOfTasks += item.Questions.Count;

            return numberOfTasks;
        }

        public void LoadLegends() =>
            _legends = Resources.LoadAll<LegendsStaticData>(PathLegends)
                .ToDictionary(x => x.CardNumber, x => x);

        public LegendsStaticData GetLegends(int cardNumber) =>
            _legends.TryGetValue(cardNumber, out LegendsStaticData staticData)
                ? staticData
                : null;
    }
}