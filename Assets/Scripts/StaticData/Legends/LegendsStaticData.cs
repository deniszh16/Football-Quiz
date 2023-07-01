using UnityEngine;

namespace StaticData.Legends
{
    [CreateAssetMenu(fileName = "LegendStaticData", menuName = "StaticData/Legend Static Data")]
    public class LegendsStaticData : ScriptableObject
    {
        [Header("Номер карточки")]
        public int CardNumber;

        [Header("Футболист")]
        public Legend Legend;
    }
}