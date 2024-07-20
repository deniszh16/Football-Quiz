using DZGames.Football.StaticData.Countries;
using DZGames.Football.StaticData.Facts;
using DZGames.Football.StaticData.Legends;
using DZGames.Football.StaticData.Players;

namespace DZGames.Football.Services
{
    public interface IStaticDataService
    {
        public void LoadQuestionsCountries();
        public CountriesStaticData GetCountriesCategory(int categoryNumber);
        public int GetNumberOfTasksInCountries();
        public void LoadQuestionsFacts();
        public FactsStaticData GetFactsCategory(int categoryNumber);
        public int GetNumberOfFactsCategories();
        public int GetNumberOfTasksInFacts();
        public void LoadQuestionsPlayers();
        public PlayersStaticData GetPlayersCategory(int categoryNumber);
        public int GetNumberOfTasksInPlayers();
        public void LoadLegends();
        public LegendsStaticData GetLegends(int cardNumber);
    }
}