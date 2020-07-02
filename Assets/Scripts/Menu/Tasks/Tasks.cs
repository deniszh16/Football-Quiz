using UnityEngine;

namespace Cubra
{
    [CreateAssetMenu]
    public class Tasks : ScriptableObject, IQuantity
    {
        [Header("Количество заданий по категориям")]
        [SerializeField] private int[] _tasksCategories;

        // Количество категорий
        public int QuantityCategories => _tasksCategories.Length;

        // Количество заданий в категориях
        public int this[int index] => _tasksCategories[index];
    }
}