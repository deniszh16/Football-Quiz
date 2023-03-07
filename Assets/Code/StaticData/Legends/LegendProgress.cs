using System;
using UnityEngine;

namespace Code.StaticData.Legends
{
    [Serializable]
    public class LegendProgress
    {
        [Header("Клубные достижения")]
        public string Club;
        
        [Header("Достижения в сборной")]
        public string NationalTeam;
        
        [Header("Личные достижения")]
        public string PersonalAchievements;
    }
}