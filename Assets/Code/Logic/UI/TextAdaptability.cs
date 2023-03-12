using UnityEngine;
using TMPro;

namespace Code.Logic.UI
{
    public class TextAdaptability : MonoBehaviour
    {
        [Header("Ссылки на компоненты")]
        [SerializeField] private TextMeshProUGUI _textComponent;
        
        [Header("Короткий текст")]
        [SerializeField] private string _shortText;

        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
                _textComponent.text = _shortText;
        }
    }
}