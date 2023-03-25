using UnityEngine;

namespace Code.Logic.UI
{
    public class ElementAdaptability : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private RectTransform _rectTransform;

        [Header("Позиция и размер")]
        [SerializeField] private PositionAndSize _positionAndSizeOnNarrowScreen;
        [SerializeField] private PositionAndSize _positionAndSizeOnUltraNarrowScreen;

        private void Start()
        {
            if (AspectRatio.Ratio is > 0.46f and <= 0.5f)
            {
                _rectTransform.anchoredPosition = _positionAndSizeOnNarrowScreen.Position;
                _rectTransform.sizeDelta = _positionAndSizeOnNarrowScreen.Size;
            }

            if (AspectRatio.Ratio <= 0.46f)
            {
                _rectTransform.anchoredPosition = _positionAndSizeOnUltraNarrowScreen.Position;
                _rectTransform.sizeDelta = _positionAndSizeOnUltraNarrowScreen.Size;
            }
        }
    }
}