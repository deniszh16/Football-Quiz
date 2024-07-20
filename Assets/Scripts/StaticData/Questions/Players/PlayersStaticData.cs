using System.Collections.Generic;
using UnityEngine;

namespace DZGames.Football.StaticData.Players
{
    [CreateAssetMenu(fileName = "PlayersStaticData", menuName = "StaticData/Player Static Data")]
    public class PlayersStaticData : ScriptableObject
    {
        [Header("Номер категории")]
        public int CategoryNumber;

        [Space]
        [Header("Список заданий")]
        public List<Question> Questions;
    }
}