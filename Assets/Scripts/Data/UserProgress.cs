﻿using System;

namespace DZGames.Football.Data
{
    [Serializable]
    public class UserProgress
    {
        public int Score;
        public int Coins = 1000;

        public event Action ScoreChanged;
        public event Action CoinsChanged;
        public event Action CoinsSubstracted;
        public event Action CoinsLacked;

        public CountriesData CountriesData = new();
        public FactsData FactsData = new();
        public PlayersData PlayersData = new();
        public LegendsData LegendsData = new();
        
        public AdsData AdsData = new();
        public LeaderboardData LeaderboardData = new();

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
            if (Coins < 0) Coins = 0;
            
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