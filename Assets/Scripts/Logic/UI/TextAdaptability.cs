using UnityEngine;
using TMPro;

namespace DZGames.Football.UI
{
    public class TextAdaptability : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private TextMeshProUGUI _textComponent;
        
        [Header("Короткий текст")]
        [SerializeField] private string _shortText;

        [Header("Размер текста")]
        [SerializeField] private bool _enableResizing;
        [SerializeField] private int _textSize;

        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
                _textComponent.text = _shortText;

            if (AspectRatio.Ratio <= 0.46f)
            {
                if (_enableResizing)
                    _textComponent.fontSize = _textSize;
            }
        }
    }
}