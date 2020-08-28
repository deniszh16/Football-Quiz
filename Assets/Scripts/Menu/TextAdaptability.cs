using UnityEngine;
using UnityEngine.UI;

namespace Cubra
{
    public class TextAdaptability : MonoBehaviour
    {
        [Header("Короткий текст")]
        [SerializeField] private string _shortText;

        private Text _textComponent;

        private void Awake()
        {
            _textComponent = GetComponent<Text>();
        }

        private void Start()
        {
            if (AspectRatio.Ratio <= 0.5f)
            {
                _textComponent.text = _shortText;
            }
        }
    }
}