using UnityEngine;

namespace Cubra.Players
{
    public class Category : MonoBehaviour
    {
        [Header("����� ���������")]
        [SerializeField] private int _number;

        public int Number => _number;
    }
}