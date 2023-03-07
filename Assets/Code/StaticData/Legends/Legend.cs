using System;
using UnityEngine;

namespace Code.StaticData.Legends
{
    [Serializable]
    public class Legend
    {
        [Header("Имя")]
        public string Name;
        
        [Header("Достижения")]
        public LegendProgress LegendProgress;
    }
}