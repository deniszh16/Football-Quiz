using UnityEngine;

namespace Logic.Players
{
    public class Category : MonoBehaviour
    {
        [Header("Номер категории")]
        [SerializeField] private int _number;
        
        public int Number => _number;
    }
}