using UnityEngine;

namespace DZGames.Football.Players
{
    public class Category : MonoBehaviour
    {
        public int Number => _number;
        
        [Header("Номер категории")]
        [SerializeField] private int _number;
    }
}