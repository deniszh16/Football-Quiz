using UnityEngine;

namespace Cubra
{
    public class ElementAdaptability : MonoBehaviour
    {
        [Header("Позиция на узком экране")]
        [SerializeField] private Vector2 _position;

        [Header("Размер на узком экране")]
        [SerializeField] private Vector2 _size;

        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
            {
                _rectTransform.anchoredPosition = _position;
                _rectTransform.sizeDelta = _size;
            }
        }
    }
}