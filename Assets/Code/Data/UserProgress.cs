using System;

namespace Code.Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Coins;

        public event Action ScoreChanged;
        public event Action CoinsChanged;
        public event Action CoinsSubstracted;
        public event Action CoinsLacked;

        public CountriesData CountriesData;
        public FactsData FactsData;
        public PlayersData PlayersData;
        public LegendsData LegendsData;
        
        public AdsData AdsData;
        public LeaderboardData LeaderboardData;

        public bool OldProgress;

        public UserProgress()
        {
            Coins = 1000;
            CountriesData = new CountriesData();
            FactsData = new FactsData();
            PlayersData = new PlayersData();
            LegendsData = new LegendsData();
            AdsData = new AdsData();
            LeaderboardData = new LeaderboardData();
            OldProgress = true;
        }

        public void AddScore(int value)
        {
            Score += value;
            ScoreChanged?.Invoke();
        }

        public void AddCoins(int value)
        {
            Coins += value;
            CoinsChanged?.Invoke();
        }

        public void SubtractionCoins(int value)
        {
            Coins -= value;
            if (Coins < 0)Coins = 0;
            
            CoinsChanged?.Invoke();
            CoinsSubstracted?.Invoke();
        }

        public bool CheckAmountCoins(int value)
        {
            if (Coins < value)
            {
                CoinsLacked?.Invoke();
                return false;
            }

            return true;
        }
    }
}