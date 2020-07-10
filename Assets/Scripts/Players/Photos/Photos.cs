using UnityEngine;

namespace Cubra.Players
{
    [CreateAssetMenu]
    public class Photos : ScriptableObject
    {
        [Header("Список фотографий")]
        [SerializeField] private Sprite[] _sprites;

        public Sprite this[int index] => _sprites[index];
    }
}